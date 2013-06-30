using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using XPTable.Models;

//--- Testing for GitHub

namespace pRoMiSeFingerPrintRegisterV6
{
    public partial class FormFingerPrintRegister : Form
    {
        TableModel tmlStaff;
        private int[] iListStaffRoleID;
        //-------------------------------------------------------------
        public FormFingerPrintRegister()
        {
            InitializeComponent();
        }

        private void FormFingerPrintRegister_Load(object sender, EventArgs e)
        {
            var frmLogin = new FormLogin();
            if (frmLogin.ShowDialog(this) == DialogResult.OK)
            {
                BringToFront();
                InitialTableViewStaff();
                LoadComboStaffRole();
                LoadFingerPrintStaff();
            }
            else
            {
                Application.Exit();
                return;
            }

            BringToFront();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ClassLib.conn != null)
            {
                ClassLib.dbFunc.CloseConnection(ClassLib.conn);
                ClassLib.conn.Close();
            }

            Application.Exit();
        }

        private void InitialTableViewStaff()
        {
            tblStaff.BeginUpdate();

            var colNo = new TextColumn("#", 35);
            colNo.Alignment = ColumnAlignment.Left;

            var colStaffCode        = new TextColumn("Staff Code", 100);
            colStaffCode.Editable = false;

            var colStaffName        = new TextColumn("Staff Name", tblStaff.Width-35-100-120-75-23);
            colStaffName.Editable = false;

            var colStaffRole        = new TextColumn("Staff Role", 120);
            colStaffRole.Editable = false;
            colStaffRole.Alignment = ColumnAlignment.Center;

            var colRegister        = new ButtonColumn("Register", 75);
            colRegister.Editable = false;
            colRegister.Alignment = ColumnAlignment.Center;

            tblStaff.ColumnModel = new ColumnModel(new Column[] { colNo, colStaffCode, colStaffName, colStaffRole, colRegister });

            tblStaff.HeaderRenderer = new XPTable.Renderers.GradientHeaderRenderer();
            tblStaff.EndUpdate();

            tmlStaff = new TableModel();
            tmlStaff.RowHeight += 14;
            tblStaff.TableModel = tmlStaff;
        }

        private void LoadComboStaffRole()
        {
            CbxRole.Items.Clear();
            string szQuery = "SELECT * FROM StaffRole WHERE Deleted=0";
            DataTable dt = ClassLib.dbFunc.List(szQuery, ClassLib.conn);
            CbxRole.Items.Add("-- Show all role --");
            iListStaffRoleID = new int[dt.Rows.Count];
            for (int i=0; i<dt.Rows.Count; i++)
            {
                CbxRole.Items.Add(dt.Rows[i]["StaffRoleName"].ToString());
                iListStaffRoleID[i] = int.Parse(dt.Rows[i]["StaffRoleID"].ToString());
            }
            CbxRole.SelectedIndex = 0;
        }

        private void LoadFingerPrintStaff()
        {
            tmlStaff.Rows.Clear();
            string szQuery = "SELECT COUNT(FingerID) AS FingerID, A.*, B.* " +
                             "FROM Staffs A " +
                             "LEFT JOIN StaffRole B ON A.StaffRoleID=B.StaffRoleID " +
                             "LEFT JOIN FingerPrintDetailStaff C ON A.StaffID=C.StaffID " +
                             //"LEFT JOIN StaffAccess D ON A.StaffRoleID=D.StaffRoleID " +
                             "WHERE A.Deleted=0 AND B.Deleted=0 AND (C.Deleted=0 Or C.Deleted is NULL) " +
                             "AND A.StaffRoleID=B.StaffRoleID " +
                             " AND A.Activated=1 ";
                             //"AND D.ProductLevelID=" + ClassLib.iShopID;
            if (CbxRole.SelectedIndex > 0)
                szQuery += " AND A.StaffRoleID=" + iListStaffRoleID[CbxRole.SelectedIndex - 1];
            szQuery += " GROUP BY A.StaffID";
            DataTable dt = ClassLib.dbFunc.List(szQuery, ClassLib.conn);
            tblStaff.BeginUpdate();
            for (int i=0; i<dt.Rows.Count; i++)
            {
                var row = new Row();
                var cell = new Cell[tblStaff.ColumnCount];
                cell[0] = new Cell((i+1) + ".");
                cell[1] = new Cell(dt.Rows[i]["StaffCode"].ToString());
                cell[2] = new Cell(dt.Rows[i]["StaffFirstName"]+" "+dt.Rows[i]["StaffLastName"]);
                cell[3] = new Cell(dt.Rows[i]["StaffRoleName"].ToString());
                cell[4] = new Cell("Register");

                bool bIsRegister = int.Parse(dt.Rows[i]["FingerID"].ToString()) > 0 ? true : false;
                row.BackColor = bIsRegister ? Color.White : Color.MistyRose;
                row.Cells.AddRange(cell);
                row.Tag = int.Parse(dt.Rows[i]["StaffID"].ToString());
                tmlStaff.Rows.Add(row);
            }
            tblStaff.EndUpdate();

        }

        private void CbxRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFingerPrintStaff();
        }

        private void tblStaff_CellButtonClicked(object sender, XPTable.Events.CellButtonEventArgs e)
        {
            int iStaffID = (int)tblStaff.TableModel.Rows[e.Cell.Row.Index].Tag;
            if (e.Column == 4)      // Register FingerPrint
            {
                //------------------------------------------------

                if (!ClassLib.bStaffCanEditFingerPrint)
                {
                    // No permission
                    var msgDlg = new FormMsgDlg("< Invalid Permission >",
                                                "Warning! You cannot modify finger print of staff.\r\n\r\nPlease check your permission.",
                                                eIcon.Warning);
                    msgDlg.ShowDialog(this);
                    return;
                }
                //-------------------------------------------------------------------------------------
                var frmRegister = new FormRegister(iStaffID);
                frmRegister.ShowDialog(this);
                LoadFingerPrintStaff();
            }
        }

        private void btnTestFingerPrint_Click(object sender, EventArgs e)
        {
            var frmTestFingerPrint = new FormTestFingerPrint();
            frmTestFingerPrint.ShowDialog(this);
        }


    }
}
