Imports MySql.Data.MySqlClient
Imports POSMySQL.POSControl
Imports pRoMiSeUtil.pRoMiSeUtil
Imports POSTypeClass
Imports System.Text

Friend Class JobOrderSQL

    Public Shared Function GetSelectComponentForPrint(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal orderID As Integer, ByVal productID As Integer, ByVal saleMode As Integer, ByVal saleDate As Date, _
    ByVal printComAmount As Boolean, ByVal isTempTable As Boolean) As DataTable
        Dim strSQL As String
        Dim dtResult As DataTable
        Dim strIn, strTable As String
        Dim i, selSaleMode As Integer
        Dim strDate As String
        strDate = FormatDateForMySQL(saleDate)
        Select Case saleMode
            Case POSType.SALEMODE_DINEIN
                selSaleMode = saleMode
            Case Else       'Check Component For current SaleMode First
                Dim dtComponent As DataTable
                strSQL = "Select pcg.* " & _
                        "From PComponentGroup pcg, ProductComponent pc " & _
                        "Where ((pcg.StartDate <= " & strDate & " And pcg.EndDate >= " & strDate & ") Or " & _
                        " (pcg.StartDate <= " & strDate & " And pcg.EndDate Is Null)) AND " & _
                        " pcg.ProductID = " & productID & " And pcg.SaleMode = " & saleMode & " AND pcg.PGroupID = pc.PGroupID AND " & _
                        " pcg.ProductID = pc.ProductID AND pcg.SaleMode = pc.SaleMode "
                dtComponent = dbUtil.List(strSQL, objCnn)
                'No Component In Current SaleMode --> Use Dine In SaleMode
                If dtComponent.Rows.Count = 0 Then
                    selSaleMode = POSType.SALEMODE_DINEIN
                Else
                    selSaleMode = saleMode
                End If
        End Select

        strSQL = "Select MaterialID " & _
                 "From OrderUnSelectComponent " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " AND OrderDetailID = " & orderID
        dtResult = dbUtil.List(strSQL, objCnn)
        strIn = ""
        For i = 0 To dtResult.Rows.Count - 1
            strIn &= dtResult.Rows(i)("MaterialID") & ", "
        Next i
        If strIn <> "" Then
            strIn = " AND pc.MaterialID NOT IN (" & Mid(strIn, 1, Len(strIn) - 2) & ") "
        End If
        If isTempTable = True Then
            strTable = "OrderDetailFront "
        Else
            strTable = "OrderDetail "
        End If
        If printComAmount = False Then
            strSQL = "Select m.MaterialID, m.MaterialName " & _
                     "From " & strTable & " od, Materials m, PComponentGroup pg, ProductComponent pc " & _
                     "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                     " od.OrderDetailID = " & orderID & " AND od.ProductID = pg.ProductID AND " & _
                     " pg.StartDate <= " & strDate & " AND pg.EndDate Is NULL AND pg.PGroupID = pc.PGroupID AND " & _
                     " pg.SaleMode = " & selSaleMode & " AND pg.ProductID = pc.ProductID AND pg.SaleMode = pc.SaleMode AND " & _
                     " m.MaterialID = pc.MaterialID AND m.Deleted = 0 " & strIn & _
                     "Order by m.MaterialName "
        Else
            strSQL = "Select m.MaterialID, m.MaterialName, pc.MaterialAmount, us.UnitSmallName " & _
                     "From " & strTable & " od, Materials m, PComponentGroup pg, ProductComponent pc, UnitSmall us " & _
                     "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                     " od.OrderDetailID = " & orderID & " AND od.ProductID = pg.ProductID AND " & _
                     " pg.StartDate <= " & strDate & " AND pg.EndDate Is NULL AND pg.PGroupID = pc.PGroupID AND " & _
                     " pg.SaleMode = " & selSaleMode & " AND pg.ProductID = pc.ProductID AND pg.SaleMode = pc.SaleMode AND " & _
                     " m.MaterialID = pc.MaterialID AND m.Deleted = 0 " & strIn & " AND pc.UnitSmallID = us.UnitSmallID " & _
                     "Order by m.MaterialName "
        End If
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Overloads Shared Function GetPrinterList(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection) As DataTable
        Dim strSQL As String
        strSQL = "Select PrinterID, PrinterName, PrinterDeviceName " & _
                 "From Printers " & _
                 "Where Deleted = 0 " & _
                 "Order by PrinterID "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Overloads Shared Function GetPrinterList(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal notPrinterID As Integer) As DataTable
        Dim strSQL As String
        strSQL = "Select PrinterID, PrinterName, PrinterDeviceName " & _
                 "From Printers " & _
                 "Where Deleted = 0 AND PrinterID <> " & notPrinterID & " " & _
                 "Order by PrinterID "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function GetListOfOrderDetailID(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
        ByVal transID As Integer, ByVal transComID As Integer, ByVal groupOfOrderID As String, ByVal orderStatusID As String) As String
        Dim strSQL As String
        Dim i As Integer
        Dim strOrderID As String
        Dim dtResult As DataTable
        strSQL = "Select OrderDetailID From OrderDetailFront " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " AND OrderStatusID IN (" & orderStatusID & ") "
        If groupOfOrderID <> "" Then
            strSQL &= " AND OrderDetailID IN (" & groupOfOrderID & ") "
        End If
        dtResult = dbUtil.List(strSQL, objCnn)
        strOrderID = ""
        For i = 0 To dtResult.Rows.Count - 1
            strOrderID &= dtResult.Rows(i)("OrderDetailID") & ", "
        Next i
        If strOrderID = "" Then
            Return ""
        Else
            Return Mid(strOrderID, 1, Len(strOrderID) - 2)
        End If
    End Function

    Public Shared Function GetNameAndNumberOfCustomer(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal transID As Integer, ByVal transComID As Integer, ByVal isTempTable As Boolean) As DataTable
        Dim strSQL As String
        Dim strName As String
        If isTempTable = True Then
            strName = " OrderTransactionFront "
        Else
            strName = " OrderTransaction "
        End If
        strSQL = "Select TransactionName, NoCustomer, QueueName, MemberDiscountID, CommStaffID, CalculateProductFromMainPrice, TransactionStatusID " & _
                 "From " & strName & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function ListOfOrderForPrint(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal jobOrderComID As Integer) As DataTable
        Dim strSQL As String
        Dim dtResult As DataTable
        Dim strIn As String
        Dim i As Integer

        'Create Temperary table
        strSQL = "Drop Table If Exists PrepareOrderForPrintTemp" & jobOrderComID & "; "
        dbUtil.sqlExecute(strSQL, objCnn)
        strSQL = "Create Table If Not Exists PrepareOrderForPrintTemp" & jobOrderComID & " (OrderDetailID int NOT NULL," & _
                 " PrinterID varchar(50), PrintGroup int, ProductSetType int, OrderStatusID int, OrderLinkID int NOT NULL, " & _
                 " PrintFlexibleProductFromItsOwnPrinter int, OrderBookNo varchar(50), OrderNumber int); "
        dbUtil.sqlExecute(strSQL, objCnn)

        'OrderStatusID = 2 for Not print and 7 for Not Confirm Bonus Product
        strSQL = "Insert INTO PrepareOrderForPrintTemp" & jobOrderComID & _
                " Select od.OrderDetailID, p.PrinterID as PrinterID, p.PrintGroup, od.ProductSetType, " & _
                " OrderStatusID, 0, p.PrintFlexibleProductFromItsOwnPrinter, '', 0 " & _
                 "From OrderDetailFront od, Products p " & _
                 "Where od.ProductID = p.ProductID AND od.PrintStatus IN (0,1) AND " & _
                 "  od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " " & _
                 " UNION " & _
                 "Select od.OrderDetailID, od.PrinterID, od.PrintGroup, od.ProductSetType, OrderStatusID, 0, 0, '', 0 " & _
                 "From OrderDetailFront od " & _
                 "Where od.ProductID = 0 AND od.PrintStatus IN (0,1) AND " & _
                 "  od.TransactionID = " & transID & " AND od.ComputerID = " & transComID
        dbUtil.sqlExecute(strSQL, objCnn)

        'Make LinkID for Flexible Product and Comment --> For PrinterID Setting
        strSQL = "Select ol.OrderDetailID, ol.OrderLinkID as OrderLinkID, pd.PrintFlexibleProductFromItsOwnPrinter, " & _
                 " pd.PrintGroup " & _
                 "From PrepareOrderForPrintTemp" & jobOrderComID & " pd, OrderProductSetLinkDetailFront ol " & _
                 "Where ol.TransactionID = " & transID & " AND ol.ComputerID = " & transComID & " AND " & _
                 " pd.OrderDetailID = ol.OrderLinkID AND pd.ProductSetType IN (1,6,7) " & _
                 " UNION "
        'Link for Product Comment
        strSQL &= "Select oc.OrderDetailID, oc.CommentForOrderID as OrderLinkID, pd.PrintFlexibleProductFromItsOwnPrinter, " & _
                 " pd.PrintGroup " & _
                 "From OrderCommentLinkFront oc, PrepareOrderForPrintTemp" & jobOrderComID & " pd " & _
                 "Where pd.OrderDetailID = oc.CommentForOrderID AND oc.TransactionID = " & transID & " AND " & _
                 " oc.ComputerID = " & transComID & " AND oc.CommentForOrderID <> 0 " & _
                 "Order by OrderLinkID "
        dtResult = dbUtil.List(strSQL, objCnn)
        If dtResult.Rows.Count > 0 Then
            Dim curID, curPrintFlex As Integer
            strIn = ""
            curID = dtResult.Rows(0)("OrderLinkID")
            curPrintFlex = dtResult.Rows(0)("PrintFlexibleProductFromItsOwnPrinter")
            For i = 0 To dtResult.Rows.Count - 1
                If curID <> dtResult.Rows(i)("OrderLinkID") Then
                    If strIn <> "" Then
                        strIn = "(" & Mid(strIn, 1, Len(strIn) - 2) & ")"
                        strSQL = "Update PrepareOrderForPrintTemp" & jobOrderComID & " " & _
                                 "Set OrderLinkID = " & curID & " , " & _
                                 " PrintFlexibleProductFromItsOwnPrinter = " & curPrintFlex & " " & _
                                "Where OrderDetailID IN " & strIn
                        dbUtil.sqlExecute(strSQL, objCnn)
                    End If
                    curID = dtResult.Rows(i)("OrderLinkID")
                    curPrintFlex = dtResult.Rows(i)("PrintFlexibleProductFromItsOwnPrinter")
                    strIn = ""
                End If
                strIn &= dtResult.Rows(i)("OrderDetailID") & ", "
            Next i
            'Update Last group
            If strIn <> "" Then
                strIn = "(" & Mid(strIn, 1, Len(strIn) - 2) & ")"
                strSQL = "Update PrepareOrderForPrintTemp" & jobOrderComID & " " & _
                         "Set OrderLinkID = " & curID & ", " & _
                         " PrintFlexibleProductFromItsOwnPrinter = " & curPrintFlex & " " & _
                        "Where OrderDetailID IN " & strIn
                dbUtil.sqlExecute(strSQL, objCnn)
            End If
        End If
        strSQL = "Select pd.* " & _
                 "From PrepareOrderForPrintTemp" & jobOrderComID & " pd " & _
                "Order by PrinterID, OrderLinkID, OrderDetailID "
        dtResult = dbUtil.List(strSQL, objCnn)

        strSQL = "Drop Table If Exists PrepareOrderForPrintTemp" & jobOrderComID & "; "
        dbUtil.sqlExecute(strSQL, objCnn)
        Return dtResult
    End Function

    Public Shared Function GetOrderForDisplayInSelectOwnPrinter(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
     ByVal transComID As Integer, ByVal strOrderID As String) As DataTable
        Dim strSQL As String
        strSQL = "Select p.ProductName, od.Amount as ProductAmount, od.OrderDetailID " & _
                 "From OrderDetailFront od, Products p " & _
                 "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                 " od.OrderDetailID IN (" & strOrderID & ") AND od.ProductID <> 0 AND od.ProductID = p.ProductID " & _
                 " UNION " & _
                 "Select od.OtherFoodName as ProductName, od.Amount as ProductAmount, od.OrderDetailID " & _
                 "From OrderDetailFront od " & _
                 "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                 " od.OrderDetailID IN (" & strOrderID & ") AND od.ProductID = 0 " & _
                 "Order by OrderDetailID "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function GetTransactionTotalPriceForOrderDetail(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal notIncludeOrderID As String, ByVal useForCalculateServiceCharge As Boolean, _
    ByVal isTempTable As Boolean) As Decimal
        Dim strSQL As String
        Dim dtResult As DataTable
        Dim strTable As String
        If isTempTable = True Then
            strTable = "OrderDetailFront "
        Else
            strTable = "OrderDetail "
        End If
        strSQL = "Select Sum(Amount * Price) as SummaryPrice " & _
                 "From " & strTable & _
                 "Where ComputerID = " & transComID & " AND TransactionID = " & transID & " AND OrderStatusID NOT IN (3,4) "
        If notIncludeOrderID <> "" Then
            strSQL &= "AND OrderDetailID NOT IN (" & notIncludeOrderID & ") "
        End If
        If useForCalculateServiceCharge = True Then
            strSQL &= " AND HasServiceCharge = 1 "
        End If
        dtResult = dbUtil.List(strSQL, objCnn)
        If dtResult.Rows.Count = 0 Then
            Return 0
        ElseIf Not IsDBNull(dtResult.Rows(0)("SummaryPrice")) Then
            Return dtResult.Rows(0)("SummaryPrice")
        Else
            Return 0
        End If
    End Function

    Public Shared Function GetTransactionTotalPriceAndDiscountDetail(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal notIncludeOrderID As String, ByVal isTempTable As Boolean) As DataTable
        Dim strSQL As String
        If isTempTable = True Then
            strSQL = "Select Sum(CalculatePrice) as SalePrice , Sum(FoodTotalPrice) as TotalPrice " & _
                     "From OrderDetailPriceCalculateTemp " & _
                     "Where TransactionID = " & transID & " AND ComputerID = " & transComID
            If notIncludeOrderID <> "" Then
                strSQL &= " AND OrderID NOT IN (" & notIncludeOrderID & ") "
            End If
        Else
            strSQL = "Select Sum(SalePrice) as SalePrice , Sum(TotalPrice) as TotalPrice " & _
                     "From OrderDiscountDetail " & _
                     "Where TransactionID = " & transID & " AND ComputerID = " & transComID
            If notIncludeOrderID <> "" Then
                strSQL &= " AND OrderDetailID NOT IN (" & notIncludeOrderID & ") "
            End If
        End If
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function GetNoOfSubmitOrderFromTransaction(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal isTempTable As Boolean) As Integer
        Dim strSQL As String
        Dim strTable As String
        Dim dtResult As DataTable
        If isTempTable = True Then
            strTable = "OrderTransactionFront "
        Else
            strTable = "OrderTransaction "
        End If
        strSQL = "Select CouponDiscountTypeID From " & strTable & _
                 "Where ComputerID = " & transComID & " AND TransactionID = " & transID
        dtResult = dbUtil.List(strSQL, objCnn)
        If dtResult.Rows.Count = 0 Then
            Return 1
        Else
            Return dtResult.Rows(0)("CouponDiscountTypeID") + 1
        End If
    End Function

    Public Shared Function UpdateNoOfSubmitOrderFromTransaction(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal isTempTable As Boolean) As Integer
        Dim strSQL As String
        Dim strTable As String
        If isTempTable = True Then
            strTable = "OrderTransactionFront "
        Else
            strTable = "OrderTransaction "
        End If
        strSQL = "Update " & strTable & " Set CouponDiscountTypeID = CouponDiscountTypeID + 1 " & _
                 "Where ComputerID = " & transComID & " AND TransactionID = " & transID
        Return dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function DeleteDataInPrinterDetailTemp(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer) As Integer
        Dim strSQL As String
        strSQL = "Delete From OrderPrinterDetailTemp Where TransactionID = " & transID & " AND ComputerID = " & transComID
        dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function InsertAllTransactionDataIntoPrinterDetailTemp(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal groupOfOrderID As String, ByVal replacePrinterID() As String, ByVal fromTempTable As Boolean) As Integer
        Dim orderBuild As StringBuilder
        Dim strSQL, strTable As String
        Dim strInsert As String
        Dim i, j As Integer
        Dim dtResult As DataTable
        Dim strPrinterID() As String

        If fromTempTable = True Then
            strTable = "OrderDetailFront "
        Else
            strTable = "OrderDetail "
        End If
        strSQL = "Select OrderDetailID, PrinterID From " & strTable & _
                 " Where TransactionID = " & transID & " AND ComputerID = " & transComID
        If groupOfOrderID <> "" Then
            strSQL &= " AND OrderDetailID IN (" & groupOfOrderID & ") "
        End If
        dtResult = dbUtil.List(strSQL, objCnn)
        'Clear All Printer From OrderPrinterDetailTemp
        DeleteDataInPrinterDetailTemp(dbUtil, objCnn, transID, transComID)

        orderBuild = New StringBuilder
        For i = 0 To dtResult.Rows.Count - 1
            'OrderDetailID, ComputerID, TranactionID
            strInsert = dtResult.Rows(i)("OrderDetailID") & ", " & transID & ", " & transComID & ", "
            'Use PrinterID From Order
            If replacePrinterID.Length = 0 Then
                If Not IsDBNull(dtResult.Rows(i)("PrinterID")) Then
                    strTable = dtResult.Rows(i)("PrinterID")
                Else
                    strTable = ""
                End If
                strPrinterID = Split(strTable, ",")

                For j = 0 To strPrinterID.Length - 1
                    If IsNumeric(strPrinterID(j)) Then
                        strTable = "(" & strInsert & strPrinterID(j) & "), "
                        orderBuild.Append(strTable)
                    End If
                Next j
            Else
                'Use PrinterID From ReplacePrinterList --> Replace PrinterID From Order
                For j = 0 To replacePrinterID.Length - 1
                    If IsNumeric(replacePrinterID(j)) Then
                        strTable = "(" & strInsert & replacePrinterID(j) & "), "
                        orderBuild.Append(strTable)
                    End If
                Next j
            End If
        Next i

        strTable = orderBuild.ToString
        If strTable <> "" Then
            strInsert = "Insert INTO OrderPrinterDetailTemp(OrderDetailID, TransactionID, ComputerID, PrinterID) VALUES"
            strSQL = strInsert & Mid(strTable, 1, Len(strTable) - 2)
            dbUtil.sqlExecute(strSQL, objCnn)
        End If
    End Function

    Public Shared Function GetOrderForPrint(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal strPrintOrderID As String, ByVal hasBookRecord As Integer, _
    ByVal hasCheckerSystem As Integer, ByVal displayPriceAtProductSetParent As Boolean, _
    ByVal orderFromComID As Integer, ByRef dtSpaStaffDetail As DataTable, _
    ByRef dtComment As DataTable, ByVal isSplitTransaction As Boolean, _
    ByVal isPrinterFromPrinterByTableZone As Boolean, ByVal productNameInLangID As Integer, ByVal fromTempTable As Boolean, _
    ByVal dropPrintOrderDetailTempTableAfterUse As Boolean, ByVal printToKitchen As Integer) As DataTable
        Dim strSQL, strTableName As String
        Dim dtResult As DataTable
        Dim strIn As String
        Dim i As Integer
        Dim columnNameForProductName As String

        Select Case productNameInLangID
            Case 0
                columnNameForProductName = "ProductName"
            Case Else
                columnNameForProductName = "ProductName" & productNameInLangID
        End Select

        'Create Temperary table
        strSQL = "Drop Table If Exists PrintOrderDetailTemp" & orderFromComID & "; "
        dbUtil.sqlExecute(strSQL, objCnn)
        strSQL = "Create Table If Not Exists PrintOrderDetailTemp" & orderFromComID & " (OrderDetailID int NOT NULL," & _
                 " ProductID int NOT NULL, ProductName varchar(100), Amount decimal(14,2), Price decimal(18,4), " & _
                 " PrinterID int, PrintGroup tinyint, PrinterName varchar(100), PrinterProperty varchar(200), PrinterStatus tinyint, " & _
                 " ProductSetType int, OrderStatusID int, PrintStatus int, Comment text, DurationTime int, StartTime datetime, " & _
                 " SubRoomID int, PrintOrdering smallint, OrderLinkID int NOT NULL, ProductLinkName varchar(100), " & _
                 " PrintFlexibleProductFromItsOwnPrinter tinyint, OrderBookNo varchar(50), OrderNumber int, " & _
                 " ProcessOrderNo int, NoReprintOrder int, SplitNo int NOT NULL DEFAULT '-1', SaleMode tinyint); "
        dbUtil.sqlExecute(strSQL, objCnn)

        'Insert Data into PrintOrderDetailTemp --> Has BookRecord need to join with OrderBookRecord Table
        'isPrinterFromPrinterByTableZone = True, Use Printer From PrinterByTableZone table not in Printers
        If isPrinterFromPrinterByTableZone = True Then
            'Delete Data In PrinterByTableZoneForPrintTemp
            strSQL = "Delete From PrinterByTableZoneForPrintTemp Where ComputerID = " & orderFromComID
            dbUtil.sqlExecute(strSQL, objCnn)

            If fromTempTable = True Then
                strTableName = "OrderDetailFront "
            Else
                strTableName = "OrderDetail "
            End If

            strSQL = "Insert INTO PrinterByTableZoneForPrintTemp(ComputerID, TableID, PrinterID, PrinterDeviceName, PrinterProperty, PrinterStatus) " & _
                     "Select Distinct " & orderFromComID & ", od.OrderTableID, pz.PrinterID, pz.PrinterDeviceName, pz.PrinterProperty, pz.PrinterStatus " & _
                     "From " & strTableName & " od, OrderPrinterDetailTemp op, TableNo t, PrinterByTableZone pz " & _
                     "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                     " od.OrderDetailID IN (" & strPrintOrderID & ") AND od.OrderTableID = t.TableID AND " & _
                     " od.TransactionID = op.TransactionID AND od.ComputerID = op.ComputerID AND od.OrderDetailID = op.OrderDetailID AND " & _
                     " t.ZoneID = pz.ZoneID AND op.PrinterID = pz.PrinterID " & _
                     " UNION " & _
                     "Select Distinct " & orderFromComID & ", od.OrderTableID, op.PrinterID, prnt.PrinterDeviceName as PrinterName, " & _
                     " prnt.PrinterDeviceNameFor98, prnt.PrinterStatus " & _
                     "From " & strTableName & " od JOIN OrderPrinterDetailTemp op ON " & _
                     " od.TransactionID = op.TransactionID AND od.ComputerID = op.ComputerID AND od.OrderDetailID = op.OrderDetailID " & _
                     " JOIN Printers prnt ON op.PrinterID = prnt.PrinterID " & _
                     " LEFT OUTER JOIN TableNo t ON od.OrderTableID = t.TableID " & _
                     " LEFT OUTER JOIN PrinterByTableZone pz ON t.ZoneID = pz.ZoneID AND pz.PrinterID = op.PrinterID " & _
                     "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                     " od.OrderDetailID IN (" & strPrintOrderID & ") AND pz.PrinterID IS NULL "
            dbUtil.sqlExecute(strSQL, objCnn)

            If hasBookRecord = 1 Then
                If fromTempTable = True Then
                    strTableName = "OrderDetailFront od, OrderBookRecordFront obr "
                Else
                    strTableName = "OrderDetail od, OrderBookRecord obr "
                End If
                strSQL = "Insert INTO PrintOrderDetailTemp" & orderFromComID & "(OrderDetailID, ProductID, ProductName, " & _
                         " Amount, Price, PrinterID, PrintGroup, PrinterName, PrinterProperty, PrinterStatus, PrinterStatus, " & _
                         " ProductSetType, OrderStatusID, PrintStatus, " & _
                         " Comment, DurationTime, StartTime, SubRoomID, PrintOrdering, OrderLinkID, ProductLinkName, " & _
                         " PrintFlexibleProductFromItsOwnPrinter, OrderBookNo, OrderNumber, ProcessOrderNo, NoReprintOrder, SplitNo, SaleMode) " & _
                        "Select od.OrderDetailID, od.ProductID, p." & columnNameForProductName & ", od.Amount, " & _
                        " od.Price, op.PrinterID, od.PrintGroup, prnt.PrinterDeviceName, prnt.PrinterProperty, prnt.PrinterStatus, print.PrinterStatus, " & _
                        " od.ProductSetType, od.OrderStatusID, od.PrintStatus, od.Comment, od.DurationTime, od.StartTime, " & _
                        " od.SubRoomID, p.PrintOrdering, 0, '', p.PrintFlexibleProductFromItsOwnPrinter, " & _
                        " obr.OrderBookNo, obr.OrderNumber, -1, od.NoReprintOrder, -1, od.SaleMode " & _
                        "From " & strTableName & ", OrderPrinterDetailTemp op, Products p, PrinterByTableZoneForPrintTemp prnt " & _
                        "Where p.ProductID = od.ProductID AND od.OrderDetailID IN (" & strPrintOrderID & ") AND " & _
                        " od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                        " od.TransactionID = obr.TransactionID AND od.ComputerID = obr.ComputerID AND " & _
                        " od.OrderDetailID = obr.OrderDetailID AND od.ProductSetType NOT IN (14,15) AND " & _
                        " od.TransactionID = op.TransactionID AND od.ComputerID = op.ComputerID AND od.OrderDetailID = op.OrderDetailID AND " & _
                        " op.PrinterID = prnt.PrinterID AND prnt.ComputerID = " & orderFromComID & " AND " & _
                        " od.OrderTableID = prnt.TableID AND od.PrintStatus IN (1,2) AND od.ProductID <> 0 " & _
                        " UNION " & _
                        "Select od.OrderDetailID, od.ProductID, od.OtherFoodName, od.Amount, od.Price, op.PrinterID, " & _
                        " od.PrintGroup, prnt.PrinterDeviceName, prnt.PrinterProperty, print.PrinterStatus, od.ProductSetType, od.OrderStatusID, " & _
                        " od.PrintStatus,od.Comment, od.DurationTime, " & _
                        " od.StartTime, od.SubRoomID, 10000, 0, '', 0, obr.OrderBookNo, obr.OrderNumber, " & _
                        " -1, od.NoReprintOrder, -1, od.SaleMode " & _
                        "From " & strTableName & ", OrderPrinterDetailTemp op, PrinterByTableZoneForPrintTemp prnt " & _
                        "Where od.ProductID = 0 AND od.OrderDetailID IN (" & strPrintOrderID & ") AND " & _
                        " od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                        " od.TransactionID = obr.TransactionID AND od.ComputerID = obr.ComputerID AND " & _
                        " od.OrderDetailID = obr.OrderDetailID AND od.TransactionID = op.TransactionID AND od.ComputerID = op.ComputerID AND " & _
                        " od.OrderDetailID = op.OrderDetailID AND op.PrinterID = prnt.PrinterID AND " & _
                        " prnt.ComputerID = " & orderFromComID & " AND od.OrderTableID = prnt.TableID AND od.PrintStatus IN (1,2) "
            Else
                If fromTempTable = True Then
                    strTableName = "OrderDetailFront "
                Else
                    strTableName = "OrderDetail "
                End If
                strSQL = "Insert INTO PrintOrderDetailTemp" & orderFromComID & "(OrderDetailID, ProductID, ProductName, " & _
                         " Amount, Price, PrinterID, PrintGroup, PrinterName, PrinterProperty, PrinterStatus, ProductSetType, OrderStatusID, PrintStatus, " & _
                         " Comment, DurationTime, StartTime, SubRoomID, PrintOrdering, OrderLinkID, ProductLinkName, " & _
                         " PrintFlexibleProductFromItsOwnPrinter, OrderBookNo, OrderNumber, ProcessOrderNo, NoReprintOrder, SplitNo, SaleMode) " & _
                         "Select od.OrderDetailID, od.ProductID, p." & columnNameForProductName & ", od.Amount, od.Price, " & _
                         " op.PrinterID, od.PrintGroup, prnt.PrinterDeviceName, prnt.PrinterProperty, prnt.PrinterStatus, " & _
                         " od.ProductSetType, od.OrderStatusID, od.PrintStatus, od.Comment, od.DurationTime, od.StartTime, " & _
                         " od.SubRoomID, p.PrintOrdering, 0, '', p.PrintFlexibleProductFromItsOwnPrinter, " & _
                         " '', 0, -1, od.NoReprintOrder, -1, od.SaleMode " & _
                         "From " & strTableName & " od, OrderPrinterDetailTemp op, Products p, PrinterByTableZoneForPrintTemp prnt " & _
                         "Where p.ProductID = od.ProductID AND od.OrderDetailID IN (" & strPrintOrderID & ") AND " & _
                         " od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                         " od.TransactionID = op.TransactionID AND od.ComputerID = op.ComputerID AND od.OrderDetailID = op.OrderDetailID AND " & _
                         " od.ProductSetType NOT IN (14,15) AND od.OrderTableID = prnt.TableID AND " & _
                         " prnt.ComputerID = " & orderFromComID & " AND op.PrinterID = prnt.PrinterID AND od.ProductID <> 0 " & _
                         " UNION " & _
                         "Select od.OrderDetailID, od.ProductID, od.OtherFoodName, od.Amount, od.Price, " & _
                         " op.PrinterID, od.PrintGroup, prnt.PrinterDeviceName, prnt.PrinterProperty, prnt.PrinterStatus, " & _
                         " od.ProductSetType, od.OrderStatusID, od.PrintStatus, od.Comment, od.DurationTime, " & _
                         " od.StartTime, od.SubRoomID, 10000, 0, '', 0, '', 0, -1, od.NoReprintOrder, -1, od.SaleMode " & _
                         "From " & strTableName & " od, OrderPrinterDetailTemp op, PrinterByTableZoneForPrintTemp prnt " & _
                         "Where od.ProductID = 0 AND od.OrderDetailID IN (" & strPrintOrderID & ") AND " & _
                         " od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                         " prnt.ComputerID = " & orderFromComID & " AND od.TransactionID = op.TransactionID AND " & _
                         " od.ComputerID = op.ComputerID AND od.OrderDetailID = op.OrderDetailID AND op.PrinterID = prnt.PrinterID AND " & _
                         " od.OrderTableID = prnt.TableID "
            End If
        Else
            If hasBookRecord > 0 Then
                If fromTempTable = True Then
                    strTableName = "OrderDetailFront od, OrderBookRecordFront obr"
                Else
                    strTableName = "OrderDetail od, OrderBookRecord obr"
                End If
                strSQL = "Insert INTO PrintOrderDetailTemp" & orderFromComID & "(OrderDetailID, ProductID, ProductName, " & _
                         " Amount, Price, PrinterID, PrintGroup, PrinterName, PrinterProperty, PrinterStatus, ProductSetType, OrderStatusID, PrintStatus, " & _
                         " Comment, DurationTime, StartTime, SubRoomID, PrintOrdering, OrderLinkID, ProductLinkName, " & _
                         " PrintFlexibleProductFromItsOwnPrinter, OrderBookNo, OrderNumber, ProcessOrderNo, NoReprintOrder, SplitNo, SaleMode) " & _
                        "Select od.OrderDetailID, od.ProductID, p." & columnNameForProductName & ", od.Amount, " & _
                        " od.Price, op.PrinterID, od.PrintGroup, prnt.PrinterDeviceName as PrinterName, prnt.PrinterDeviceNameFor98, prnt.PrinterStatus, " & _
                        " od.ProductSetType, od.OrderStatusID, od.PrintStatus, od.Comment, od.DurationTime, od.StartTime, " & _
                        " od.SubRoomID, p.PrintOrdering, 0, '', p.PrintFlexibleProductFromItsOwnPrinter, " & _
                        " obr.OrderBookNo, obr.OrderNumber, -1, od.NoReprintOrder, -1, od.SaleMode " & _
                        "From " & strTableName & ", OrderPrinterDetailTemp op, Products p, Printers prnt " & _
                        "Where p.ProductID = od.ProductID AND od.OrderDetailID IN (" & strPrintOrderID & ") AND " & _
                        "  od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                        "  od.TransactionID = obr.TransactionID AND od.ComputerID = obr.ComputerID AND " & _
                        "  od.OrderDetailID = obr.OrderDetailID AND od.ProductSetType NOT IN (14,15) AND " & _
                        "  od.TransactionID = op.TransactionID AND od.ComputerID = op.ComputerID AND od.OrderDetailID = op.OrderDetailID AND " & _
                        "  od.PrinterID = prnt.PrinterID AND od.ProductID <> 0 " & _
                        " UNION " & _
                        "Select od.OrderDetailID, od.ProductID, od.OtherFoodName, od.Amount, od.Price, " & _
                        " op.PrinterID, od.PrintGroup, prnt.PrinterDeviceName as PrinterName, prnt.PrinterDeviceNameFor98, prnt.PrinterStatus, " & _
                        " od.ProductSetType, od.OrderStatusID, od.PrintStatus, od.Comment, od.DurationTime, " & _
                        " od.StartTime, od.SubRoomID, 10000, 0, '', 0, obr.OrderBookNo, obr.OrderNumber, " & _
                        " -1, od.NoReprintOrder, -1, od.SaleMode " & _
                        "From " & strTableName & ", OrderPrinterDetailTemp op, Printers prnt " & _
                        "Where od.ProductID = 0 AND od.OrderDetailID IN (" & strPrintOrderID & ") AND " & _
                        "  od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                        "  od.TransactionID = obr.TransactionID AND od.ComputerID = obr.ComputerID AND " & _
                        "  od.OrderDetailID = obr.OrderDetailID AND od.TransactionID = op.TransactionID AND od.ComputerID = op.ComputerID AND " & _
                        "  od.OrderDetailID = op.OrderDetailID AND op.PrinterID = prnt.PrinterID "
            Else
                If fromTempTable = True Then
                    strTableName = "OrderDetailFront od"
                Else
                    strTableName = "OrderDetail od"
                End If
                strSQL = "Insert INTO PrintOrderDetailTemp" & orderFromComID & "(OrderDetailID, ProductID, ProductName, " & _
                         " Amount, Price, PrinterID, PrintGroup, PrinterName, PrinterProperty, PrinterStatus, ProductSetType, OrderStatusID, PrintStatus, " & _
                         " Comment, DurationTime, StartTime, SubRoomID, PrintOrdering, OrderLinkID, ProductLinkName, " & _
                         " PrintFlexibleProductFromItsOwnPrinter, OrderBookNo, OrderNumber, ProcessOrderNo, NoReprintOrder, SplitNo, SaleMode) " & _
                         "Select od.OrderDetailID, od.ProductID, p." & columnNameForProductName & ", od.Amount, " & _
                         " od.Price, op.PrinterID, od.PrintGroup, prnt.PrinterDeviceName as PrinterName, prnt.PrinterDeviceNameFor98, prnt.PrinterStatus, " & _
                         " od.ProductSetType, od.OrderStatusID, od.PrintStatus, od.Comment, od.DurationTime, od.StartTime, " & _
                         " od.SubRoomID, p.PrintOrdering, 0, '', p.PrintFlexibleProductFromItsOwnPrinter, " & _
                         " '', 0, -1, od.NoReprintOrder, -1, od.SaleMode " & _
                         "From " & strTableName & ", OrderPrinterDetailTemp op, Products p, Printers prnt " & _
                         "Where p.ProductID = od.ProductID AND od.OrderDetailID IN (" & strPrintOrderID & ") AND " & _
                         " od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                         " od.ProductSetType NOT IN (14,15) AND od.TransactionID = op.TransactionID AND od.ComputerID = op.ComputerID AND " & _
                         " od.OrderDetailID = op.OrderDetailID AND op.PrinterID = prnt.PrinterID AND od.ProductID <> 0 " & _
                         " UNION " & _
                         "Select od.OrderDetailID, od.ProductID, od.OtherFoodName, od.Amount, od.Price, " & _
                         "op.PrinterID, od.PrintGroup, prnt.PrinterDeviceName as PrinterName, prnt.PrinterDeviceNameFor98, prnt.PrinterStatus, " & _
                         "od.ProductSetType, od.OrderStatusID, od.PrintStatus, od.Comment, od.DurationTime, " & _
                         " od.StartTime, od.SubRoomID, 10000, 0, '', 0, '', 0, -1, od.NoReprintOrder, -1, od.SaleMode " & _
                         "From " & strTableName & ", OrderPrinterDetailTemp op, Printers prnt " & _
                         "Where od.ProductID = 0 AND od.OrderDetailID IN (" & strPrintOrderID & ") AND " & _
                         " od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                         " od.TransactionID = op.TransactionID AND od.ComputerID = op.ComputerID AND od.OrderDetailID = op.OrderDetailID AND " & _
                         " op.PrinterID = prnt.PrinterID "
            End If
        End If
        dbUtil.sqlExecute(strSQL, objCnn)

        'Delete Comment Order From PrintOrderDetailTemp
        If fromTempTable = True Then
            strSQL = "Select OrderDetailID From OrderCommentLinkFront " & _
                     "Where TransactionID = " & transID & " AND ComputerID = " & transComID
        Else
            strSQL = "Select OrderDetailID From OrderCommentWithPriceDetail " & _
                     "Where TransactionID = " & transID & " AND ComputerID = " & transComID
        End If
        dtResult = dbUtil.List(strSQL, objCnn)
        strIn = ""
        For i = 0 To dtResult.Rows.Count - 1
            strIn &= dtResult.Rows(i)("OrderDetailID") & ", "
        Next i
        If strIn <> "" Then
            strSQL = "Delete From PrintOrderDetailTemp" & orderFromComID & " Where OrderDetailID IN (" & Mid(strIn, 1, Len(strIn) - 2) & ")"
            dbUtil.sqlExecute(strSQL, objCnn)
        End If
        'Set SplitNo In Transaction
        If isSplitTransaction = True Then
            Dim curSplitNo As Integer
            If fromTempTable = True Then
                strTableName = "SplitOrderDetailFront sod, SplitOrderTransactionFront sot "
            Else
                strTableName = "SplitOrderDetail sod, SplitOrderTransaction sot "
            End If
            strSQL = "Select pd.OrderDetailID, sot.SplitNo " & _
                     "From PrintOrderDetailTemp" & orderFromComID & " pd, " & strTableName & _
                     "Where sod.SplitTransactionID = sot.SplitTransactionID AND sod.SplitComputerID = sot.SplitComputerID AND " & _
                     " sod.OriginalTransactionID = " & transID & " AND sod.OriginalComputerID = " & transComID & " AND " & _
                     " sod.OriginalOrderDetailID = pd.OrderDetailID " & _
                     "Order by sot.SplitNo "
            dtResult = dbUtil.List(strSQL, objCnn)
            If dtResult.Rows.Count > 0 Then
                curSplitNo = dtResult.Rows(0)("SplitNo")
            End If
            strIn = ""
            For i = 0 To dtResult.Rows.Count - 1
                If curSplitNo <> dtResult.Rows(i)("SplitNo") Then
                    strIn = Mid(strIn, 1, Len(strIn) - 2)
                    strSQL = "Update PrintOrderDetailTemp" & orderFromComID & _
                             " Set SplitNo = " & curSplitNo & _
                             " Where OrderDetailID IN (" & strIn & ") "
                    dbUtil.sqlExecute(strSQL, objCnn)
                    curSplitNo = dtResult.Rows(i)("SplitNo")
                    strIn = ""
                End If
                strIn &= dtResult.Rows(i)("OrderDetailID") & ", "
            Next i
            If strIn <> "" Then
                strIn = Mid(strIn, 1, Len(strIn) - 2)
                strSQL = "Update PrintOrderDetailTemp" & orderFromComID & _
                         " Set SplitNo = " & curSplitNo & _
                         " Where OrderDetailID IN (" & strIn & ") "
                dbUtil.sqlExecute(strSQL, objCnn)
            End If
        End If

        'Check for ProductSet = 1,6,7,-2 --> Flexible Product, ProductSet, ProductSetInPackage
        If fromTempTable = True Then
            strSQL = "Select ol.OrderDetailID, ol.OrderLinkID, pd.ProductName, pd.PrintFlexibleProductFromItsOwnPrinter, " & _
                     " pd.PrintGroup, pd.ProductSetType, pd.Amount, pd.Price " & _
                     "From PrintOrderDetailTemp" & orderFromComID & " pd, OrderProductSetLinkDetailFront ol " & _
                     "Where ol.TransactionID = " & transID & " AND ol.ComputerID = " & transComID & " AND " & _
                     " pd.OrderDetailID = ol.OrderLinkID AND pd.ProductSetType IN (1,6,7,-2) "
            If printToKitchen = 2 Then
                strSQL &= " UNION " & _
                       "Select ol.OrderDetailID, ol.OrderLinkID, pd.ProductName, pd.PrintFlexibleProductFromItsOwnPrinter, " & _
                       " pd.PrintGroup, pd.ProductSetType, pd.Amount, pd.Price " & _
                       "From PrintOrderDetailTemp" & orderFromComID & " pd, OrderProductLinkDetail ol " & _
                       "Where ol.TransactionID = " & transID & " AND ol.ComputerID = " & transComID & " AND " & _
                       " pd.OrderDetailID = ol.OrderLinkID AND pd.ProductSetType IN (1,6,7,-2) "
            End If
            strSQL &= "Order by OrderLinkID "
        Else
            strSQL = "Select ol.OrderDetailID, ol.OrderLinkID, pd.ProductName, pd.PrintFlexibleProductFromItsOwnPrinter, " & _
                     " pd.PrintGroup, pd.ProductSetType, pd.Amount, pd.Price " & _
                     "From PrintOrderDetailTemp" & orderFromComID & " pd, OrderSpaProductSetLinkDetail ol " & _
                     "Where ol.TransactionID = " & transID & " AND ol.ComputerID = " & transComID & " AND " & _
                     " pd.OrderDetailID = ol.OrderLinkID AND pd.ProductSetType IN (1,6,7,-2) " & _
                     " UNION " & _
                     "Select ol.OrderDetailID, ol.OrderLinkID, pd.ProductName, pd.PrintFlexibleProductFromItsOwnPrinter, " & _
                     " pd.PrintGroup, pd.ProductSetType, pd.Amount, pd.Price " & _
                     "From PrintOrderDetailTemp" & orderFromComID & " pd, OrderProductLinkDetail ol " & _
                     "Where ol.TransactionID = " & transID & " AND ol.ComputerID = " & transComID & " AND " & _
                     " pd.OrderDetailID = ol.OrderLinkID AND pd.ProductSetType IN (1,6,7,-2) " & _
                     "Order by OrderLinkID "
        End If
        dtResult = dbUtil.List(strSQL, objCnn)
        If dtResult.Rows.Count > 0 Then
            Dim strDelProductSet As String
            Dim curID, curPrintFlex As Integer
            Dim curProductName As String

            strDelProductSet = ""
            curID = dtResult.Rows(0)("OrderLinkID")
            curProductName = dtResult.Rows(0)("ProductName")
            curPrintFlex = dtResult.Rows(0)("PrintFlexibleProductFromItsOwnPrinter")
            strIn = ""
            For i = 0 To dtResult.Rows.Count - 1
                If curID <> dtResult.Rows(i)("OrderLinkID") Then
                    If strIn <> "" Then
                        strIn = "(" & Mid(strIn, 1, Len(strIn) - 2) & ")"
                        strSQL = "Update PrintOrderDetailTemp" & orderFromComID & " " & _
                                 "Set OrderLinkID = " & curID & " , ProductLinkName = '" & ReplaceSuitableStringForSQL(curProductName) & "', " & _
                                 " PrintFlexibleProductFromItsOwnPrinter = " & curPrintFlex & ", ProductSetType = -6 " & _
                                "Where OrderDetailID IN " & strIn
                        dbUtil.sqlExecute(strSQL, objCnn)
                    End If
                    curID = dtResult.Rows(i)("OrderLinkID")
                    curProductName = dtResult.Rows(i)("ProductName")
                    curPrintFlex = dtResult.Rows(i)("PrintFlexibleProductFromItsOwnPrinter")
                    strIn = ""
                End If
                strIn &= dtResult.Rows(i)("OrderDetailID") & ", "

                Select Case dtResult.Rows(i)("ProductSetType")
                    Case POSType.PRODUCTTYPE_PRODUCTSET_IN_PACKAGE ', POSType.PRODUCTTYPE_PRODUCTSET
                        strDelProductSet &= dtResult.Rows(i)("OrderDetailID") & ", "
                End Select
            Next i
            If strIn <> "" Then
                strIn = "(" & Mid(strIn, 1, Len(strIn) - 2) & ")"
                strSQL = "Update PrintOrderDetailTemp" & orderFromComID & " " & _
                         "Set OrderLinkID = " & curID & " , ProductLinkName = '" & ReplaceSuitableStringForSQL(curProductName) & "', " & _
                         " PrintFlexibleProductFromItsOwnPrinter = " & curPrintFlex & ", ProductSetType = -6 " & _
                        "Where OrderDetailID IN " & strIn
                dbUtil.sqlExecute(strSQL, objCnn)
            End If

            'Delete Spa ProductSet and ProductSetInPackage --> Because  It doesn't need to print its Name Into printer
            ' Except Normal ProductSet (Can use direct OrderLinkID because it may be normal ProductSet)
            If strDelProductSet <> "" Then
                strDelProductSet = Mid(strDelProductSet, 1, Len(strDelProductSet) - 2)
                strSQL = "Select Distinct OrderLinkID From PrintOrderDetailTemp" & orderFromComID & " " & _
                         "Where OrderDetailID IN (" & strDelProductSet & ") "
                dtResult = dbUtil.List(strSQL, objCnn)
                strDelProductSet = ""
                For i = 0 To dtResult.Rows.Count - 1
                    strDelProductSet &= dtResult.Rows(i)("OrderLinkID") & ", "
                Next i
                If strDelProductSet <> "" Then
                    strDelProductSet = Mid(strDelProductSet, 1, Len(strDelProductSet) - 2)
                    strSQL = "Delete From PrintOrderDetailTemp" & orderFromComID & " " & _
                             "Where OrderDetailID IN (" & strDelProductSet & ") "
                    dbUtil.sqlExecute(strSQL, objCnn)
                End If
            End If

            'For Display Product Price At Its Parent --> Set Price To Its Parent
            If displayPriceAtProductSetParent = True Then
                Dim dclPrice As Decimal
                strSQL = "Select OrderLinkID, OrderDetailID, Amount, Price " & _
                          "From PrintOrderDetailTemp" & orderFromComID & " " & _
                          "Where OrderLinkID <> 0 AND ProductSetType IN (" & POSType.PRODUCTTYPE_PRODUCT_IN_FLEXIBLEPRODUCTSET & ", " & _
                          POSType.PRODUCTTYPE_PRODUCT_IN_PRODUCTSET & ") " & _
                          "Order By OrderLinkID "
                dtResult = dbUtil.List(strSQL, objCnn)
                If dtResult.Rows.Count <> 0 Then
                    curID = dtResult.Rows(0)("OrderLinkID")
                    dclPrice = 0
                    strIn = ""
                    For i = 0 To dtResult.Rows.Count - 1
                        If curID <> dtResult.Rows(i)("OrderLinkID") Then
                            If strIn <> "" Then
                                strIn = "(" & Mid(strIn, 1, Len(strIn) - 2) & ")"
                                strSQL = "Update PrintOrderDetailTemp" & orderFromComID & " " & _
                                         "Set Price = " & dclPrice & "/ Amount " & _
                                        "Where OrderDetailID = " & curID & " AND Amount <> 0 "
                                dbUtil.sqlExecute(strSQL, objCnn)
                                strSQL = "Update PrintOrderDetailTemp" & orderFromComID & " " & _
                                         "Set Price = 0 " & _
                                        "Where OrderDetailID IN " & strIn
                                dbUtil.sqlExecute(strSQL, objCnn)
                            End If
                            curID = dtResult.Rows(i)("OrderLinkID")
                            strIn = ""
                            dclPrice = 0
                        End If
                        dclPrice += (dtResult.Rows(i)("Amount") * dtResult.Rows(i)("Price"))
                        strIn &= dtResult.Rows(i)("OrderDetailID") & ", "
                    Next i
                    If strIn <> "" Then
                        strIn = "(" & Mid(strIn, 1, Len(strIn) - 2) & ")"
                        strSQL = "Update PrintOrderDetailTemp" & orderFromComID & " " & _
                                 "Set Price = " & dclPrice & "/ Amount " & _
                                "Where OrderDetailID = " & curID & " AND Amount <> 0 "
                        dbUtil.sqlExecute(strSQL, objCnn)
                        strSQL = "Update PrintOrderDetailTemp" & orderFromComID & " " & _
                                 "Set Price = 0 " & _
                                "Where OrderDetailID IN " & strIn
                        dbUtil.sqlExecute(strSQL, objCnn)
                    End If
                End If
            End If
        End If

        'Update ProcessNo/ Delete Order In ProcessNo
        If hasCheckerSystem <> POSType.CHECKER_NOCHECKER Then
            If fromTempTable = True Then
                strSQL = "Select od.OrderDetailID, op.OrderNo, op.PrinterID " & _
                         "From OrderDetailFront od, OrderProcessDetailFront op " & _
                         "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                         " od.ProcessID = op.ProcessID AND od.ProcessID <> 0 AND op.SubProcessID = 0 "
            Else
                'Process Detail For RealTable --> Can be In ProcessDetailFront and ProcessDetail
                strSQL = "Select od.OrderDetailID, op.OrderNo, op.PrinterID " & _
                         "From OrderDetail od, OrderProcessDetailFront op " & _
                         "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                         " od.ProcessID = op.ProcessID AND od.ProcessID <> 0 AND op.SubProcessID = 0 " & _
                         " UNION " & _
                         "Select od.OrderDetailID, op.OrderNo, op.PrinterID " & _
                         "From OrderDetail od, OrderProcessDetail op " & _
                         "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                         " od.ProcessID = op.ProcessID AND od.ProcessID <> 0 AND op.SubProcessID = 0 "
            End If
            dtResult = dbUtil.List(strSQL, objCnn)
            'Update ProcessOrderNo In PrintOrderDetailTemp
            For i = 0 To dtResult.Rows.Count - 1
                strSQL = "Update PrintOrderDetailTemp" & orderFromComID & " " & _
                         "Set ProcessOrderNo = " & dtResult.Rows(i)("OrderNo") & " " & _
                         "Where OrderDetailID = " & dtResult.Rows(i)("OrderDetailID") & " AND PrinterID = " & dtResult.Rows(i)("PrinterID")
                dbUtil.sqlExecute(strSQL, objCnn)
            Next i
        End If

        'Get OrderDetail for print with staff and room detail
        strSQL = "Select pd.*, sr.SubRoomNumber, t.TableName as RoomName, t.Capacity as RoomCapacity, " & _
                     " -1 as PrintRecordNo, pd.Amount as TotalAmount, s.PositionPrefix, s.PrefixTextPrinting " & _
                     "From PrintOrderDetailTemp" & orderFromComID & " pd JOIN SaleMode s ON pd.SaleMode = s.SaleModeID " & _
                     "  LEFT OUTER JOIN SubRoom sr ON " & _
                     "  pd.SubRoomID = sr.SubRoomID LEFT OUTER JOIN TableNo t ON " & _
                     "  sr.RoomID = t.TableID " & _
                    "Order by PrinterID, PrintGroup DESC, pd.SplitNo, OrderLinkID, PrintOrdering, OrderDetailID "
        dtResult = dbUtil.List(strSQL, objCnn)

        If fromTempTable = True Then
            strTableName = "OrderStaffDetailFront "
        Else
            strTableName = "OrderStaffDetail "
        End If
        strSQL = "Select pd.OrderDetailID, s.StaffCode, s.StaffFirstName, s.StaffLastName, " & _
                     " os.StartTime, os.DurationTime " & _
                     "From PrintOrderDetailTemp" & orderFromComID & " pd, " & strTableName & " os, Staffs s " & _
                     "Where pd.OrderDetailID = os.OrderDetailID AND os.TransactionID = " & transID & " AND " & _
                     " os.ComputerID = " & transComID & " AND os.StaffID = s.StaffID " & _
                    "Order by OrderDetailID "
        dtSpaStaffDetail = dbUtil.List(strSQL, objCnn)

        If fromTempTable = True Then
            strSQL = "Select Distinct oc.CommentForOrderID, p." & columnNameForProductName & " as Comment, oc.Amount, " & _
                     " oc.ProductSetType, od.Price, oc.OrderDetailID, s.PositionPrefix, s.PrefixTextPrinting " & _
                     "From OrderCommentLinkFront oc, PrintOrderDetailTemp" & orderFromComID & " po, Products p, " & _
                     " OrderDetailFront od, SaleMode s " & _
                     "Where oc.CommentForOrderID = po.OrderDetailID AND oc.TransactionID = " & transID & " AND " & _
                     " oc.ComputerID = " & transComID & " AND oc.ProductID = p.ProductID AND od.TransactionID = oc.TransactionID AND " & _
                     " od.ComputerID = oc.ComputerID AND od.OrderDetailID = oc.OrderDetailID AND od.SaleMode = s.SaleModeID " & _
                     "Order by oc.CommentForOrderID, oc.ProductSetType DESC "
        Else
            strSQL = "Select Distinct oc.OrderDetailID as CommentForOrderID, p." & columnNameForProductName & " as Comment, oc.Amount, " & _
                     " " & POSType.PRODUCTTYPE_COMMENT & " as ProductSetType, 0.00 as Price, 0 as OrderDetailID, " & _
                     " s.PositionPrefix, s.PrefixTextPrinting " & _
                     "From OrderCommentDetail oc, PrintOrderDetailTemp" & orderFromComID & " po, Products p " & _
                     "Where oc.OrderDetailID = po.OrderDetailID AND oc.TransactionID = " & transID & " AND " & _
                     " oc.ComputerID = " & transComID & " AND oc.CommentID = p.ProductID AND pd.SaleMode = s.SaleModeID " & _
                     " UNION " & _
                     "Select Distinct oc.OrderLinkID as CommentForOrderID, p." & columnNameForProductName & " as Comment, od.Amount, " & _
                     " oc.ProductSetType, od.Price, oc.OrderDetailID, s.PositionPrefix, s.PrefixTextPrinting " & _
                     "From OrderCommentWithPriceDetail oc, PrintOrderDetailTemp" & orderFromComID & " po, Products p, " & _
                     " OrderDetail od, SaleMode s " & _
                     "Where oc.OrderLinkID = po.OrderDetailID AND oc.TransactionID = " & transID & " AND " & _
                     " oc.ComputerID = " & transComID & " AND oc.ProductID = p.ProductID AND od.TransactionID = oc.TransactionID AND " & _
                     " od.ComputerID = oc.ComputerID AND od.OrderDetailID = oc.OrderDetailID AND od.SaleMode = s.SaleModeID " & _
                     "Order by CommentForOrderID, ProductSetType DESC "
        End If
        dtComment = dbUtil.List(strSQL, objCnn)

        If dropPrintOrderDetailTempTableAfterUse = True Then
            strSQL = "Drop Table If Exists PrintOrderDetailTemp" & orderFromComID & "; "
            dbUtil.sqlExecute(strSQL, objCnn)
        End If
        DeleteDataInPrinterDetailTemp(dbUtil, objCnn, transID, transComID)
        Return dtResult
    End Function

    Public Shared Function GetOrderForPrintSummaryPrice(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal strPrintOrderID As String, ByVal dtCommentForSummary As DataTable, _
    ByVal productNameInLangID As Integer, ByVal isGroupProductAmount As Boolean, _
    ByVal notIncludeProductInPrinterID As String, ByVal groupSummaryAmountForPrinterID As String, _
    ByVal isIncludeComment As Boolean, ByVal displayPriceAtProductSetParent As Boolean, _
    ByVal fromTempTable As Boolean, ByVal orderFromComID As Integer) As DataTable
        Dim strSQL, strOrderWithComment, strCommentSQL As String
        Dim strTableName As String
        Dim columnNameForProductName, strNotIncludePrinterID As String
        Dim strSummaryAmountIn, strSummaryAmountNotIn As String
        Dim dtResult As DataTable
        Dim i As Integer

        strOrderWithComment = ""
        If isIncludeComment = True Then
            For i = 0 To dtCommentForSummary.Rows.Count - 1
                strOrderWithComment &= dtCommentForSummary.Rows(i)("CommentForOrderID") & ", "
            Next i
            If strOrderWithComment <> "" Then
                strOrderWithComment = Mid(strOrderWithComment, 1, Len(strOrderWithComment) - 2)
            End If
        End If

        If fromTempTable = True Then
            strTableName = "OrderDetailFront "
        Else
            strTableName = "OrderDetail "
        End If
        Select Case productNameInLangID
            Case 0
                columnNameForProductName = "ProductName"
            Case Else
                columnNameForProductName = "ProductName" & productNameInLangID
        End Select
        If notIncludeProductInPrinterID = "" Then
            strNotIncludePrinterID = " "
        Else
            strNotIncludePrinterID = " AND od.PrinterID NOT IN (" & notIncludeProductInPrinterID & ") "
        End If
        If groupSummaryAmountForPrinterID <> "" Then
            strSummaryAmountIn = " AND ((od.PrinterID IN (" & groupSummaryAmountForPrinterID & ")) AND (od.ProductSetType NOT IN (" & _
                                    POSType.PRODUCTTYPE_PRODUCT_IN_FLEXIBLEPRODUCTSET & ", " & POSType.PRODUCTTYPE_PRODUCT_IN_PRODUCTSET & _
                                    ", " & POSType.PRODUCTTYPE_GROUP_OF_FLEXIBLEPRODUCTSET & ", " & POSType.PRODUCTTYPE_FLEXIBLEPRODUCTSET & "))) "
            'strSummaryAmountIn = " AND od.PrinterID IN (" & groupSummaryAmountForPrinterID & ") "
            strSummaryAmountNotIn = " AND ((od.PrinterID NOT IN (" & groupSummaryAmountForPrinterID & ")) OR (od.ProductSetType IN (" & _
                                    POSType.PRODUCTTYPE_PRODUCT_IN_FLEXIBLEPRODUCTSET & ", " & POSType.PRODUCTTYPE_PRODUCT_IN_PRODUCTSET & _
                                    ", " & POSType.PRODUCTTYPE_GROUP_OF_FLEXIBLEPRODUCTSET & ", " & POSType.PRODUCTTYPE_FLEXIBLEPRODUCTSET & "))) "
        Else
            strSummaryAmountIn = " "
            strSummaryAmountNotIn = " "
        End If

        If isGroupProductAmount = True Then
            If strOrderWithComment <> "" Then
                strCommentSQL = " AND od.OrderDetailID NOT IN (" & strOrderWithComment & ") "
            Else
                strCommentSQL = " "
            End If
            strSQL = "Select p." & columnNameForProductName & " as ProductName, Sum(od.Amount) as Amount, od.Price, od.ProductSetType, " & _
                     "od.SubmitOrderDateTime, -1 As OrderLinkID, s.PositionPrefix, s.PrefixTextPrinting, p.ProductOrdering, " & _
                     "pd.ProductDeptOrdering, pg.ProductGroupOrdering, Min(od.OrderDetailID) as OrderDetailID, po.Price as FlexPrice " & _
                     "From " & strTableName & " od INNER JOIN Products p ON  od.ProductID = p.ProductID  " & _
                     " INNER JOIN SaleMode s ON od.SaleMode = s.SaleModeID " & _
                     " INNER JOIN ProductDept pd ON p.ProductDeptID = pd.ProductDeptID INNER JOIN ProductGroup pg ON pg.ProductGroupID = pd.ProductGroupID " & _
                     " LEFT OUTER JOIN PrintOrderDetailTemp" & orderFromComID & " po ON od.OrderDetailID = po.OrderDetailID " & _
                     "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                     " od.OrderDetailID IN (" & strPrintOrderID & ") AND od.ProductID <> 0 " & strNotIncludePrinterID & _
                     strCommentSQL & _
                     "Group By p.ProductID, p." & columnNameForProductName & ", od.Price, od.ProductSetType, od.SubmitOrderDateTime, " & _
                     " s.PositionPrefix, s.PrefixTextPrinting, p.ProductOrdering, pd.ProductDeptOrdering, pg.ProductGroupOrdering, " & _
                     " po.Price " & _
                     " UNION " & _
                     "Select od.OtherFoodName as ProductName, Sum(od.Amount) as Amount, od.Price, od.ProductSetType, od.SubmitOrderDateTime, " & _
                     " -1 As OrderLinkID, s.PositionPrefix, s.PrefixTextPrinting, 10000 as ProductOrdering, 10000 as ProductDeptOrdering, " & _
                     " 10000 as ProductGroupOrdering, Min(od.OrderDetailID) as OrderDetailID, 0 as FlexPrice " & _
                     "From " & strTableName & " od, SaleMode s " & _
                     "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                     " od.OrderDetailID IN (" & strPrintOrderID & ") AND od.ProductID = 0 AND od.SaleMode = s.SaleModeID " & strNotIncludePrinterID & _
                     strCommentSQL & _
                     "Group By od.OtherFoodName, od.Price, od.ProductSetType, od.SubmitOrderDateTime, s.PositionPrefix, s.PrefixTextPrinting "
            'Order With Comment
            If strOrderWithComment <> "" Then
                strCommentSQL = " AND od.OrderDetailID IN (" & strOrderWithComment & ") "
                strSQL &= " UNION " & _
                     "Select p." & columnNameForProductName & " as ProductName, od.Amount, od.Price, od.ProductSetType, " & _
                     "od.SubmitOrderDateTime, -1 As OrderLinkID, s.PositionPrefix, s.PrefixTextPrinting, p.ProductOrdering, " & _
                     "pd.ProductDeptOrdering, pg.ProductGroupOrdering, od.OrderDetailID, po.Price as FlexPrice " & _
                     "From " & strTableName & " od INNER JOIN Products p ON od.ProductID = p.ProductID " & _
                     " INNER JOIN SaleMode s ON od,SaleMode = s.SaleModeID " & _
                     " INNER JOIN ProductDept pd ON p.ProductDeptID = pd.ProductDeptID INNER JOIN ProductGroup pg ON pg.ProductGroupID = pd.ProductGroupID " & _
                     " LEFT OUTER JOIN PrintOrderDetailTemp" & orderFromComID & " po ON od.OrderDetailID = po.OrderDetailID " & _
                     "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                     " od.OrderDetailID IN (" & strPrintOrderID & ") AND od.ProductID <> 0 " & strNotIncludePrinterID & _
                     strCommentSQL & _
                     " UNION " & _
                     "Select od.OtherFoodName as ProductName, od.Amount, od.Price, od.ProductSetType, od.SubmitOrderDateTime, " & _
                     " -1 As OrderLinkID, s.PositionPrefix, s.PrefixTextPrinting, 10000 as ProductOrdering, 10000 as ProductDeptOrdering, " & _
                     " 10000 as ProductGroupOrdering, od.OrderDetailID, 0 as FlexPrice " & _
                     "From " & strTableName & " od, SaleMode s " & _
                     "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                     " od.OrderDetailID IN (" & strPrintOrderID & ") AND od.ProductID = 0 AND od.SaleMode = s.SaleModeID " & strNotIncludePrinterID & _
                     strCommentSQL
            End If
            strSQL &= " Order By ProductGroupOrdering, ProductDeptOrdering, ProductOrdering, OrderDetailID, ProductName, Price "

        Else
            strSQL = "Select p." & columnNameForProductName & " as ProductName , od.OrderDetailID, od.Amount, od.Price, od.ProductSetType, " & _
                    " od.SubmitOrderDateTime, po.OrderLinkID, s.PositionPrefix, s.PrefixTextPrinting, po.Price as FlexPrice " & _
                    "From " & strTableName & " od JOIN Products p ON od.ProductID = p.ProductID " & _
                    " JOIN SaleMode s ON od.SaleMode = s.SaleModeID " & _
                    " LEFT OUTER JOIN PrintOrderDetailTemp" & orderFromComID & " po " & _
                    " ON od.OrderDetailID = po.OrderDetailID " & _
                    "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                    " od.OrderDetailID IN (" & strPrintOrderID & ") AND od.ProductID <> 0 " & strNotIncludePrinterID & strSummaryAmountNotIn & _
                    " UNION " & _
                    "Select od.OtherFoodName as ProductName, od.OrderDetailID, od.Amount, od.Price, od.ProductSetType, od.SubmitOrderDateTime, " & _
                    " 0 as OrderLinkID, s.PositionPrefix, s.PrefixTextPrinting, 0 as FlexPrice " & _
                    "From " & strTableName & " od, SaleMode s " & _
                    "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                    " od.OrderDetailID IN (" & strPrintOrderID & ") AND od.ProductID = 0 AND od.SaleMode = s.SaleModeID " & strNotIncludePrinterID & _
                    strSummaryAmountNotIn

            If groupSummaryAmountForPrinterID <> "" Then
                If strOrderWithComment <> "" Then
                    strCommentSQL = " AND od.OrderDetailID NOT IN (" & strOrderWithComment & ") "
                Else
                    strCommentSQL = " "
                End If
                strSQL &= " UNION " & _
                        "Select p." & columnNameForProductName & " as ProductName, Min(od.OrderDetailID) * 10000 as OrderDetailID, " & _
                        " Sum(od.Amount) as Amount, od.Price, od.ProductSetType, " & _
                         "od.SubmitOrderDateTime, -1 As OrderLinkID, s.PositionPrefix, s.PrefixTextPrinting, po.Price as FlexPrice " & _
                         "From " & strTableName & " od INNER JOIN Products p ON od.ProductID = p.ProductID " & _
                         " INNER JOIN SaleMode s ON od.SaleMode = s.SaleModeID " & _
                         " LEFT OUTER JOIN PrintOrderDetailTemp" & orderFromComID & " po ON od.OrderDetailID = po.OrderDetailID " & _
                         "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                         " od.OrderDetailID IN (" & strPrintOrderID & ") AND od.ProductID <> 0 " & _
                         strNotIncludePrinterID & strSummaryAmountIn & strCommentSQL & _
                         "Group By p.ProductID, p." & columnNameForProductName & ", od.Price, od.ProductSetType, od.SubmitOrderDateTime, " & _
                         " s.PositionPrefix, s.PrefixTextPrinting, po.Price " & _
                         " UNION " & _
                         "Select od.OtherFoodName as ProductName, Min(od.OrderDetailID) as OrderDetailID, " & _
                         " Sum(od.Amount) as Amount, od.Price, od.ProductSetType, od.SubmitOrderDateTime, " & _
                         " -1 As OrderLinkID, s.PositionPrefix, s.PrefixTextPrinting, 0 as FlexPrice " & _
                         "From " & strTableName & " od, SaleMode s " & _
                         "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                         " od.OrderDetailID IN (" & strPrintOrderID & ") AND od.ProductID = 0 AND od.SaleMode = s.SaleModeID " & strNotIncludePrinterID & _
                         strSummaryAmountIn & strCommentSQL & _
                         "Group By od.OtherFoodName, od.Price, od.ProductSetType, od.SubmitOrderDateTime, s.PositionPrefix, s.PrefixTextPrinting "
                If strOrderWithComment <> "" Then
                    strCommentSQL = " AND od.OrderDetailID IN (" & strOrderWithComment & ") "
                    strSQL &= " UNION " & _
                            "Select p." & columnNameForProductName & " as ProductName , od.OrderDetailID, od.Amount, od.Price, od.ProductSetType, " & _
                            " od.SubmitOrderDateTime, po.OrderLinkID, s.PositionPrefix, s.PrefixTextPrinting, po.Price as FlexPrice " & _
                            "From " & strTableName & " od JOIN Products p ON od.ProductID = p.ProductID " & _
                            " JOIN SaleMode s ON od.SaleMode = s.SaleModeID " & _
                            " LEFT OUTER JOIN PrintOrderDetailTemp" & orderFromComID & " po " & _
                            " ON od.OrderDetailID = po.OrderDetailID " & _
                            "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                            " od.OrderDetailID IN (" & strPrintOrderID & ") AND od.ProductID <> 0 " & strNotIncludePrinterID & _
                            strSummaryAmountIn & strCommentSQL & _
                            " UNION " & _
                            "Select od.OtherFoodName as ProductName, od.OrderDetailID, od.Amount, od.Price, od.ProductSetType, od.SubmitOrderDateTime, " & _
                            " 0 as OrderLinkID, s.PositionPrefix, s.PrefixTextPrinting, 0 as FlexPrice " & _
                            "From " & strTableName & " od, SaleMode s " & _
                            "Where od.TransactionID = " & transID & " AND od.ComputerID = " & transComID & " AND " & _
                            " od.OrderDetailID IN (" & strPrintOrderID & ") AND od.ProductID = 0 AND od.SaleMode = s.SaleModeID " & strNotIncludePrinterID & _
                            strSummaryAmountIn & strCommentSQL
                End If
            End If
            strSQL &= " Order By OrderDetailID "
        End If
        dtResult = dbUtil.List(strSQL, objCnn)
        'Check For Null Value 
        For i = 0 To dtResult.Rows.Count - 1
            If IsDBNull(dtResult.Rows(i)("OrderLinkID")) Then
                dtResult.Rows(i)("OrderLinkID") = 0
            End If
            'Set Price = FlexPrice For ProductSet =7, -6
            If displayPriceAtProductSetParent = True Then
                If IsDBNull(dtResult.Rows(i)("FlexPrice")) Then
                    dtResult.Rows(i)("FlexPrice") = 0
                End If
                Select Case dtResult.Rows(i)("ProductSetType")
                    Case POSType.PRODUCTTYPE_PRODUCT_IN_FLEXIBLEPRODUCTSET
                        dtResult.Rows(i)("Price") = 0

                    Case POSType.PRODUCTTYPE_GROUP_OF_FLEXIBLEPRODUCTSET, POSType.PRODUCTTYPE_FLEXIBLEPRODUCTSET
                        dtResult.Rows(i)("Price") = dtResult.Rows(i)("FlexPrice")
                End Select
            End If
        Next i
        Return dtResult
    End Function

    Public Shared Function DropTablePrintOrderDetailTemp(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal orderFromComID As Integer) As Integer
        Dim strSQL As String
        strSQL = "Drop Table If Exists PrintOrderDetailTemp" & orderFromComID & "; "
        dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function InsertNewBookOrder(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal objTrans As MySqlTransaction, _
    ByVal orderID As Integer, ByVal transID As Integer, ByVal transComID As Integer, ByVal bookNo As String, ByVal orderNo As Integer) As Integer
        Dim strSQL As String
        'Insert New material of today
        strSQL = "Insert Into OrderBookRecordFront(OrderDetailID, TransactionID, ComputerID, OrderBookNo ,OrderNumber, OrderMonth, OrderYear) " & _
                 "Values( " & orderID & ", " & transID & ", " & transComID & ", '" & ReplaceSuitableStringForSQL(Trim(bookNo)) & _
                 "', " & orderNo & ", " & Now.Month & ", " & Now.Year & ")"
        Return dbUtil.sqlExecute(strSQL, objCnn, objTrans)
    End Function

    Public Shared Function GetTransactionQueueNo(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal comID As Integer, ByVal isTempTable As Boolean) As String
        Dim strSQL As String
        Dim dtResult As DataTable
        Dim strTable As String
        If isTempTable = True Then
            strTable = " OrderTransactionFront "
        Else
            strTable = " OrderTransaction "
        End If
        strSQL = "Select QueueName " & _
                 "From " & strTable & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & comID
        Try
            dtResult = dbUtil.List(strSQL, objCnn)
            If dtResult.Rows.Count = 0 Then
                Return ""
            ElseIf Not IsDBNull(dtResult.Rows(0)("QueueName")) Then
                Return dtResult.Rows(0)("QueueName")
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function GetCheckerPrinterIDForComputer(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal computerID As Integer) As String
        Dim strSQL, strPrinterID As String
        Dim dtResult As DataTable
        Dim i As Integer
        strSQL = "Select PrinterID From CheckerAccessPrinter Where ComputerID = " & computerID
        dtResult = dbUtil.List(strSQL, objCnn)
        strPrinterID = ""
        For i = 0 To dtResult.Rows.Count - 1
            strPrinterID &= dtResult.Rows(i)("PrinterID") & ", "
        Next i
        If strPrinterID <> "" Then
            strPrinterID = Mid(strPrinterID, 1, Len(strPrinterID) - 2)
        End If
        Return strPrinterID
    End Function

    Public Shared Function GetSummaryPriceHeaderFooter(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal shopID As Integer, _
    ByVal lineType As Integer) As DataTable
        Dim strSQL As String
        strSQL = "Select * From ReceiptHeaderFooter " & _
                 "Where ProductLevelID = " & shopID & " AND LineType = " & lineType & _
                 " Order By LineOrder "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function DeleteExistBookOrder(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal orderID As Integer, _
    ByVal transID As Integer, ByVal transComID As Integer) As Integer
        Dim strSQL As String
        strSQL = "Delete From OrderBookRecordFront " & _
                 "Where OrderDetailID = " & orderID & " AND TransactionID = " & transID & " AND ComputerID = " & transComID
        Return dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function UpdateOrderStaffComIDAndOrderPrintStatusAndSubmitTime(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal objTrans As MySqlTransaction, ByVal orderID() As Integer, ByVal transID As Integer, ByVal transComID As Integer, ByVal staffID As Integer, _
    ByVal orderComID As Integer, ByVal orderTableID As Integer, ByVal orderStatus As Integer, ByVal orderTime As String, _
    ByVal skipPrintOrder As Boolean) As Integer
        Dim strSQL, strIn As String
        Dim i As Int16
        strIn = ""
        For i = 0 To orderID.Length - 1
            strIn &= orderID(i) & ", "
        Next i
        If strIn <> "" Then
            strIn = "(" & Mid(strIn, 1, Len(strIn) - 2) & ")"
            'Update Order Status, Staff, OrderComputerID, OrderTime, PrintStatus
            If skipPrintOrder = False Then
                ' NoPrintBeforeSubmit ---> NoPrintAfterSubmit
                strSQL = "Update OrderDetailFront " & _
                         "Set OrderStaffID = " & staffID & ", OrderComputerID = " & orderComID & _
                         ", OrderTableID = " & orderTableID & ", SubmitOrderDateTime = " & orderTime & _
                         ", OrderStatusID = " & orderStatus & ", PrintStatus = 3 " & _
                         "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " AND " & _
                         " OrderDetailID IN " & strIn & " AND PrintStatus = 0 "
                dbUtil.sqlExecute(strSQL, objCnn, objTrans)
                ' NotPrintYet ----> Print
                strSQL = "Update OrderDetailFront " & _
                         "Set OrderStaffID = " & staffID & ", OrderComputerID = " & orderComID & _
                         ", OrderTableID = " & orderTableID & ", SubmitOrderDateTime = " & orderTime & _
                         ", OrderStatusID = " & orderStatus & ", PrintStatus = 2 " & _
                         "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " AND " & _
                         " OrderDetailID IN " & strIn & " AND PrintStatus = 1 "
                dbUtil.sqlExecute(strSQL, objCnn, objTrans)
            Else
                'All Order ---> NoPrintAfterSubmit
                strSQL = "Update OrderDetailFront " & _
                         "Set OrderStaffID = " & staffID & ", OrderComputerID = " & orderComID & _
                         ", OrderTableID = " & orderTableID & ", SubmitOrderDateTime = " & orderTime & _
                         ", OrderStatusID = " & orderStatus & ", PrintStatus = 3 " & _
                         "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " AND " & _
                         " OrderDetailID IN " & strIn
                dbUtil.sqlExecute(strSQL, objCnn, objTrans)
            End If
        Else
            Return 0
        End If
    End Function

    Public Shared Function UpdatePrinterForSelectOwnPrinterOrder(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal objTrans As MySqlTransaction, _
        ByVal transID As Integer, ByVal comID As Integer, ByVal orderID() As Integer, ByVal printerID As Integer) As Integer
        Dim i As Integer
        Dim strIn As String
        Dim strSQL As String
        strIn = ""
        For i = 0 To orderID.Length - 1
            strIn &= orderID(i) & ", "
        Next i
        strIn = "(" & Mid(strIn, 1, Len(strIn) - 2) & ")"
        strSQL = "Update OrderDetailFront " & _
                 "Set PrinterID = " & printerID & " " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & comID & " AND OrderDetailID IN " & strIn
        Return dbUtil.sqlExecute(strSQL, objCnn, objTrans)
    End Function

    Public Shared Function UpdatePrinterIDForPrintOrder(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal objTrans As MySqlTransaction, _
    ByVal transID As Integer, ByVal comID As Integer, ByVal strOrderID As String, ByVal printerID As String, _
    ByVal printGroup As Integer) As Integer
        Dim strSQL As String
        strSQL = "Update OrderDetailFront " & _
                 "Set PrinterID = '" & printerID & "', PrintGroup = " & printGroup & " " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & comID & " AND " & _
                 " OrderDetailID IN (" & strOrderID & ") "
        Return dbUtil.sqlExecute(strSQL, objCnn, objTrans)
    End Function

    Public Shared Function GetComputerPrinterName(ByVal dbUtil As CDBUtil, ByVal objcnn As MySqlConnection, ByVal comID As Integer) As String
        Dim strSQL As String
        Dim dtResult As DataTable
        strSQL = "Select PrinterName From ComputerName " & _
                 "Where ComputerID = " & comID
        dtResult = dbUtil.List(strSQL, objcnn)
        If dtResult.Rows.Count = 0 Then
            Return ""
        ElseIf Not IsDBNull(dtResult.Rows(0)("PrinterName")) Then
            Return dtResult.Rows(0)("PrinterName")
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetPrinterDeviceName(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal printerID As Integer) As String
        Dim strSQL As String
        Dim dtResult As DataTable
        strSQL = "Select PrinterID, PrinterDeviceName " & _
                 "From Printers " & _
                 "Where Deleted = 0 AND PrinterID = " & printerID
        dtResult = dbUtil.List(strSQL, objCnn)
        If dtResult.Rows.Count = 0 Then
            Return ""
        Else
            Return dtResult.Rows(0)("PrinterDeviceName")
        End If
    End Function

    Public Shared Function GetGroupOfPrinterName(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal groupOfPrinterID As String) As DataTable
        Dim strSQL As String
        strSQL = "Select PrinterID, PrinterName " & _
                 "From Printers " & _
                 "Where Deleted = 0 AND PrinterID IN (" & groupOfPrinterID & ") "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function GetPrinterDetailFronPrinterName(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal printerName() As String) As DataTable
        Dim strSQL As String
        Dim i As Integer
        strSQL = ""
        For i = 0 To printerName.Length - 1
            strSQL &= "'" & ReplaceSuitableStringForSQL(printerName(i)) & "', "
        Next i
        If strSQL = "" Then
            strSQL = "''"
        Else
            strSQL = Mid(strSQL, 1, Len(strSQL) - 2)
        End If
        strSQL = "Select * " & _
                 "From Printers " & _
                 "Where Deleted = 0 AND PrinterDeviceName IN (" & strSQL & ") "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function GetMaxOrderNumberInOrderBookRecord(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal comID As Integer) As Integer
        Dim strSQL As String
        Dim dtResult As DataTable
        strSQL = "Select Max(OrderNumber) as MaxOrderNumber " & _
                 "From OrderBookRecordFront " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & comID
        dtResult = dbUtil.List(strSQL, objCnn)
        If Not IsDBNull(dtResult.Rows(0)("MaxOrderNumber")) Then
            Return dtResult.Rows(0)("MaxOrderNumber") + 1
        Else
            Return 1
        End If
    End Function

    Public Shared Function GetTransactionNameAndMemberCode(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal comID As Integer) As DataTable
        Dim strSQL As String
        strSQL = "Select ot.TransactionName, m.MemberCode " & _
                 "From OrderTransactionFront ot Left Outer Join Members m " & _
                 " ON m.MemberID = ot.MemberDiscountID AND ot.MemberDiscountID <> 0 " & _
                 "Where ot.TransactionID = " & transID & " AND ot.ComputerID = " & comID
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function InsertReprintOrderHistory(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal transID As Integer, ByVal comID As Integer, ByVal orderID As Integer, ByVal tableID As Integer, _
    ByVal productID As Integer, ByVal productName As String, ByVal orderAmount As Decimal, _
    ByVal reprintComID As Integer, ByVal reprintStaffID As Integer, ByVal reprintDateTime As String, _
    ByVal frontFunctionID As Integer) As Integer
        Dim strSQL As String
        strSQL = "Insert INTO HistoryOfRePrintOrderDetail(TransactionID, ComputerID, OrderDetailID, TableID, " & _
                 " ProductID, ProductName, OrderAmount, RePrintComputerID, RePrintStaffID, RePrintDateTime, " & _
                 " FrontFunctionID) " & _
                 "VALUES(" & transID & ", " & comID & ", " & orderID & ", " & tableID & ", " & productID & ", '" & _
                 ReplaceSuitableStringForSQL(productName) & "', " & orderAmount & ", " & reprintComID & ", " & reprintStaffID & ", " & _
                 reprintDateTime & ", " & frontFunctionID & " ) "
        dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function GetReprintHeaderText(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal frontFunctionID As Integer, ByVal productLevelID As Integer) As String
        Dim strSQL As String
        Dim dtResult As DataTable
        strSQL = "Select * From ReprintOrderHeaderText " & _
                 "Where FrontFunctionID = " & frontFunctionID & _
                 " Order by ProductLevelID "
        dtResult = dbUtil.List(strSQL, objCnn)
        If dtResult.Rows.Count = 0 Then
            Return ""
        ElseIf Not IsDBNull(dtResult.Rows(0)("ReprintHeader")) Then
            Return dtResult.Rows(0)("ReprintHeader")
        Else
            Return ""
        End If
    End Function

    Public Shared Function UpdateNoReprintOrder(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal strOrderID As String, _
    ByVal noReprint As Integer, ByVal isTempTable As Boolean) As Integer
        Dim strSQL As String
        Dim strTable As String
        If isTempTable = True Then
            strTable = "OrderDetailFront "
        Else
            strTable = "OrderDetail "
        End If
        If strOrderID = "" Then
            strOrderID = "-1"
        End If
        strSQL = "Update " & strTable & _
                 "Set NoReprintOrder = NoReprintOrder + " & noReprint & " " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " AND " & _
                 " OrderDetailID IN (" & strOrderID & ") "
        Return dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function DeleteOrderPrintNoRecord(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal groupOfOrderDetailID As String) As Integer
        Dim strSQL As String
        strSQL = "Delete From OrderPrintNoRecordFront " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " AND " & _
                 " OrderDetailID In (" & groupOfOrderDetailID & ") "
        Return dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function GetMaxPrintNoRecord(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer) As Integer
        Dim strSQL As String
        Dim dtResult As DataTable
        strSQL = "Select Max(PrintNo) as MaxPrintNo " & _
                 "From OrderPrintNoRecordFront " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID
        dtResult = dbUtil.List(strSQL, objCnn)
        If dtResult.Rows.Count = 0 Then
            Return 0
        ElseIf Not IsDBNull(dtResult.Rows(0)("MaxPrintNo")) Then
            Return dtResult.Rows(0)("MaxPrintNo")
        Else
            Return 0
        End If
    End Function

    Public Shared Function InsertOrderPrintNoRecord(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
      ByVal transComID As Integer, ByVal orderDetailID As Integer, ByVal printNo As Integer) As Integer
        Dim strSQL As String
        strSQL = "Delete From OrderPrintNoRecordFront " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID & _
                 " AND OrderDetailID = " & orderDetailID
        dbUtil.sqlExecute(strSQL, objCnn)
        strSQL = "Insert INTO OrderPrintNoRecordFront(TransactionID, ComputerID, OrderDetailID, PrintNo) " & _
                 "VALUES(" & transID & "," & transComID & ", " & orderDetailID & ", " & printNo & ") "
        Return dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function GetPrintNoRecordForPrintOrder(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
       ByVal transComID As Integer, ByVal groupOfOrderDetailID As String, ByVal isTempTable As Boolean) As DataTable
        Dim strSQL As String
        Dim strTable As String
        If isTempTable = True Then
            strTable = "OrderPrintNoRecordFront "
        Else
            strTable = "OrderPrintNoRecord "
        End If
        strSQL = "Select * From " & strTable & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID
        If groupOfOrderDetailID <> "" Then
            strSQL &= " AND OrderDetailID In (" & groupOfOrderDetailID & ") "
        End If
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function GetOrderDetailIDFromOrderLink(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
    ByVal transComID As Integer, ByVal groupOfOrderLinkID As String, ByVal isTempTable As Boolean) As DataTable
        Dim strSQL As String
        If isTempTable = True Then
            strSQL = "Select OrderDetailID " & _
                    "From OrderProductSetLinkDetailFront " & _
                    "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " AND " & _
                    " OrderLinkID In (" & groupOfOrderLinkID & ") "
        Else
            strSQL = "Select OrderDetailID " & _
                     "From OrderSpaProductSetLinkDetail " & _
                     "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " AND " & _
                     " OrderLinkID In (" & groupOfOrderLinkID & ") " & _
                     " UNION " & _
                     "Select OrderDetailID " & _
                     "From OrderProductLinkDetail " & _
                     "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " AND " & _
                     " OrderLinkID In (" & groupOfOrderLinkID & ") "
        End If
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function IsLoginGrant(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal staffCode As String, _
    ByVal staffPwd As String) As DataTable
        Dim strSQL As String
        Dim strCode As String
        'Null string will cause error for MySQL
        If staffCode = "" Then
            strCode = " A "
        Else
            strCode = staffCode
        End If
        strSQL = "Select pItem.PermissionItemParam as PermissionName, sf.StaffID as StaffID, sf.StaffRoleID, " & _
                 " sf.StaffFirstName as  StaffFirstName, sf.StaffLastName as StaffLastName, sf.StaffCode as StaffCode, " & _
                 " sf.LangID, sf.Activated " & _
                 "FROM Staffs sf, StaffRole sr, StaffPermission sp, PermissionItem pItem " & _
                 "Where sf.StaffCode = " & "'" & ReplaceSuitableStringForSQL(strCode) & "' AND " & _
                 "      sf.StaffPassword = '" & ReplaceSuitableStringForSQL(staffPwd) & "' AND " & _
                 "      sf.StaffRoleID = sr.StaffRoleID AND " & _
                 "      sr.StaffRoleID = sp.StaffRoleID AND " & _
                 "      sp.PermissionItemID = pItem.PermissionItemID AND " & _
                 "      sf.Deleted = 0 "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    'Get all Staff permission
    Public Shared Function IsStaffCanAccessThisShop(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal staffRoleID As Integer, ByVal shopID As Integer) As Boolean
        Dim strSQL As String
        Dim dtResult As DataTable
        strSQL = "Select ProductLevelID From StaffAccess " & _
                 "Where ProductLevelID = " & shopID & " AND StaffRoleID = " & staffRoleID
        dtResult = dbUtil.List(strSQL, objCnn)
        If dtResult.Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function GetPrintJobOrderFieldData(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection) As DataTable
        Dim strSQL As String
        strSQL = "Show Fields From PrintJobOrderDetail "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function HasPrinterInPrinterByTableZone(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal shopID As Integer) As Boolean
        Dim strSQL As String
        Dim dtResult As DataTable
        strSQL = "Select pz.* " & _
                 "From TableZone tz, PrinterByTableZone pz " & _
                 "Where tz.ShopID = " & shopID & " AND pz.ZoneID = tz.ZoneID "
        dtResult = dbUtil.List(strSQL, objCnn)
        If dtResult.Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function UpdateZeroSaleModeInOrderToDineInSaleMode(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal transID As Integer, ByVal transComID As Integer) As Integer
        Dim strSQL As String
        strSQL = "Update OrderDetailFront Set SaleMode = " & POSType.SALEMODE_DINEIN & _
                 " Where ComputerID = " & transComID & " AND TransactionID = " & transID & " AND SaleMode = 0 "
        dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function AddDataIntoPrintJobDetail(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal groupOfJobOrder As String) As Integer
        Dim strSQL As String
        strSQL = "Insert INTO PrintJobOrderDetailFront(TransactionID, ComputerID, OrderDetailID, PrintNo, IsPrintSummary, InsertDateTime, " & _
                 " PrintDateTime, SaleDate, JobOrderFromComputerID, JobOrderStatus) " & _
                 "VALUES " & groupOfJobOrder & "; "
        dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function GenerateStringForAddPrintJobOrder(ByVal transID As Integer, ByVal transComID As Integer, _
    ByVal orderID As Integer, ByVal printNo As Integer, ByVal isPrintSummary As Integer, ByVal insertDate As String, _
    ByVal saleDate As String, ByVal fromComID As Integer, ByVal jobOrderStatus As Integer) As String
        Dim strSQL As String
        strSQL = "(" & transID & ", " & transComID & ", " & orderID & ", " & printNo & ", " & isPrintSummary & ", " & _
                insertDate & ", NULL, " & saleDate & ", " & fromComID & ", " & jobOrderStatus & ")"
        Return strSQL
    End Function

    Public Shared Function AddDataIntoPrintJobLineDetail(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal groupOfJobOrder As String) As Integer
        Dim strSQL As String
        strSQL = "Insert INTO PrintJobOrderLineDetailFront(TransactionID, ComputerID, PrintNo, LineOrder, LeftText, CenterText, RightText, " & _
                 "NameText, JobOrderLineType, UseFontType, IsRedColor, IsPrintSummary, PrinterID, PrinterName, PrinterProperty, SaleDate) " & _
                 "VALUES " & groupOfJobOrder & "; "
        dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function GenerateStringForAddPrintJobOrderLineDetail(ByVal transID As Integer, ByVal transComID As Integer, _
    ByVal printNo As Integer, ByVal lineOrder As Integer, ByVal leftText As String, ByVal centerText As String, ByVal rightText As String, _
    ByVal nameText As String, ByVal lineType As Integer, ByVal useFontType As Integer, _
    ByVal isRedColor As Integer, ByVal isPrintSummary As Integer, _
    ByVal printerID As Integer, ByVal printerName As String, ByVal printerProperty As String, ByVal saleDate As String) As String
        Dim strSQL As String
        strSQL = "(" & transID & ", " & transComID & ", " & printNo & ", " & lineOrder & " , '" & ReplaceSuitableStringForSQL(leftText) & "', '" & _
                ReplaceSuitableStringForSQL(centerText) & "', '" & ReplaceSuitableStringForSQL(rightText) & "', '" & _
                ReplaceSuitableStringForSQL(nameText) & "', " & lineType & ", " & useFontType & ", " & isRedColor & ", " & isPrintSummary & ", " & _
                printerID & ", '" & ReplaceSuitableStringForSQL(printerName) & "', '" & _
                ReplaceSuitableStringForSQL(printerProperty) & "', " & saleDate & ")"
        Return strSQL
    End Function

    Public Shared Function MovePrintJobOrderDetailToRealTable(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal dtPrintJobOrderField As DataTable, ByVal transID As Integer, ByVal transComID As Integer, ByVal groupOfPrintNo As String, _
    ByVal printDateTime As String, ByVal finishPrintDateTime As String, ByVal printFromComID As Integer, ByVal jobOrderStatus As Integer) As Integer
        Dim strSQL, strSelect, strInsert As String
        Dim strTransID As String
        strTransID = "Where TransactionID = " & transID & " AND ComputerID = " & transComID
        If groupOfPrintNo <> "" Then
            strTransID &= " AND PrintNo IN (" & groupOfPrintNo & ")"
        End If
        If POSPrintJobOrderUtilModule.HasFieldInDataTable(dtPrintJobOrderField, "FinishPrintDateTime") = True Then
            strInsert = ", FinishPrintDateTime, PrintFromComputerID "
            strSelect = ", " & finishPrintDateTime & ", " & printFromComID & " "
        Else
            strInsert = ""
            strSelect = ""
        End If
 
        'Print Job OrderDetail
        strSQL = "Insert INTO PrintJobOrderDetail(TransactionID, ComputerID, OrderDetailID, PrintNo, IsPrintSummary, InsertDateTime, PrintDateTime, " & _
                 "SaleDate, JobOrderFromComputerID, JobOrderStatus" & strInsert & ") " & _
                 "Select TransactionID, ComputerID, OrderDetailID, PrintNo, IsPrintSummary, InsertDateTime, " & printDateTime & _
                 ", SaleDate, JobOrderFromComputerID, " & jobOrderStatus & strSelect & " " & _
                 "From PrintJobOrderDetailFront " & strTransID
        dbUtil.sqlExecute(strSQL, objCnn)
        strSQL = "Delete From PrintJobOrderDetailFront " & strTransID
        dbUtil.sqlExecute(strSQL, objCnn)
        'Print Job Order Line Detail
        strSQL = "Insert INTO PrintJobOrderLineDetail(TransactionID, ComputerID, PrintNo, LineOrder, LeftText, CenterText, RightText, " & _
                 "NameText, JobOrderLineType, UseFontType, IsRedColor, IsPrintSummary, PrinterID, PrinterName, PrinterProperty, SaleDate) " & _
                 "Select TransactionID, ComputerID, PrintNo, LineOrder, LeftText, CenterText, RightText, NameText, JobOrderLineType, UseFontType, " & _
                 " IsRedColor, IsPrintSummary, PrinterID, PrinterName, PrinterProperty, SaleDate " & _
                 "From PrintJobOrderLineDetailFront " & strTransID
        dbUtil.sqlExecute(strSQL, objCnn)
        strSQL = "Delete From PrintJobOrderLineDetailFront " & strTransID
        dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function GetPrintJobOrderPrintNo(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal transID As Integer, ByVal transComID As Integer, ByVal saleDate As Date) As Integer
        Dim strSQL As String
        Dim printNo As Integer
        Dim dtResult As DataTable
        strSQL = "Select * " & _
                 "From PrintJobOrderPrintNo " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID
        dtResult = dbUtil.List(strSQL, objCnn)
        'Insert PrintNo From PrintJobOrderDetail
        If dtResult.Rows.Count = 0 Then
            printNo = 1
            'Get MaxPrintNo In PrintJobOrderDetailFront
            strSQL = "Select Max(PrintNo) as PrintNo From PrintJobOrderDetailFront Where TransactionID = " & transID & " AND ComputerID = " & transComID
            dtResult = dbUtil.List(strSQL, objCnn)
            If dtResult.Rows.Count <> 0 Then
                If Not IsDBNull(dtResult.Rows(0)("PrintNo")) Then
                    If printNo < dtResult.Rows(0)("PrintNo") Then
                        printNo = dtResult.Rows(0)("PrintNo")
                    End If
                End If
            End If
            'Get MaxPrintNo In PrintJobOrderDetail
            strSQL = "Select Max(PrintNo) as PrintNo From PrintJobOrderDetail Where TransactionID = " & transID & " AND ComputerID = " & transComID
            dtResult = dbUtil.List(strSQL, objCnn)
            If dtResult.Rows.Count <> 0 Then
                If Not IsDBNull(dtResult.Rows(0)("PrintNo")) Then
                    If printNo < dtResult.Rows(0)("PrintNo") Then
                        printNo = dtResult.Rows(0)("PrintNo")
                    End If
                End If
            End If
            strSQL = "Insert INTO PrintJobOrderPrintNo(TransactionID, ComputerID, PrintNo, SaleDate) " & _
                     "VALUES(" & transID & ", " & transComID & ", " & printNo & "," & FormatDateForMySQL(saleDate) & ") "
            dbUtil.sqlExecute(strSQL, objCnn)
        Else
            'Update PrintJobOrder
            printNo = dtResult.Rows(0)("PrintNo") + 1
            strSQL = "Update PrintJobOrderPrintNo Set PrintNo = " & printNo & " " & _
                     "Where TransactionID = " & transID & " AND ComputerID = " & transComID
            dbUtil.sqlExecute(strSQL, objCnn)
        End If
        Return printNo
    End Function

    Public Shared Function GetAllTransactionPrintJobOrderDetail(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal groupOfOrderFromComputerID As String, ByVal saleDate As Date) As DataTable
        Dim strSQL As String
        strSQL = "Select TransactionID, ComputerID, Min(InsertDateTime) as InsertDate " & _
                 "From PrintJobOrderDetailFront " & _
                 "Where SaleDate = " & FormatDateForMySQL(saleDate) & " AND JobOrderStatus <> " & POSType.JOBORDERSTATUS_PRINTSUCCESS & " "
        If groupOfOrderFromComputerID <> "" Then
            strSQL &= " AND JobOrderFromComputerID IN (" & groupOfOrderFromComputerID & ") "
        End If
        strSQL &= "Group By TransactionID, ComputerID " & _
                 "Order By InsertDate "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function GetAllTransactionPrintNoInJobOrderDetail(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal groupOfOrderFromComputerID As String, ByVal saleDate As Date) As DataTable
        Dim strSQL As String
        strSQL = "Select Distinct TransactionID, ComputerID, PrintNo, InsertDateTime " & _
                 "From PrintJobOrderDetailFront " & _
                 "Where SaleDate = " & FormatDateForMySQL(saleDate) & " AND JobOrderStatus <> " & POSType.JOBORDERSTATUS_PRINTSUCCESS & " "
        If groupOfOrderFromComputerID <> "" Then
            strSQL &= " AND JobOrderFromComputerID IN (" & groupOfOrderFromComputerID & ") "
        End If
        strSQL &= "Order By InsertDateTime "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function GetPrintJobOrderLineDetailForPrint(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal transID As Integer, ByVal transComID As Integer, ByVal isFrontTable As Boolean) As DataTable
        Dim strSQL As String
        Dim strTable As String
        If isFrontTable = True Then
            strTable = " PrintJobOrderLineDetailFront "
        Else
            strTable = " PrintJobOrderLineDetail "
        End If
        strSQL = "Select * " & _
                 "From " & strTable & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " " & _
                 "Order By PrintNo, LineOrder "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function UpdatePrintJobOrderStatus(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal transID As Integer, ByVal transComID As Integer, ByVal groupOfPrintNo As String, ByVal jobOrderStatus As Integer, _
    ByVal isFrontTable As Boolean) As Integer
        Dim strSQL As String
        Dim strTable As String
        If isFrontTable = True Then
            strTable = " PrintJobOrderDetailFront "
        Else
            strTable = " PrintJobOrderDetail "
        End If
        strSQL = "Update" & strTable & " Set JobOrderStatus = " & jobOrderStatus & " " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID
        If groupOfPrintNo <> "" Then
            strSQL &= " AND PrintNo IN (" & groupOfPrintNo & ") "
        End If
        Return dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function LockTableForJobOrderDetail(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection) As Integer
        Dim strSQL As String
        strSQL = "Lock Table PrintJobOrderDetailFront WRITE, PrintJobOrderLineDetailFront WRITE "
        dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function CheckAndAddUseFontTypeForPrintJobOrderLineDetail(ByVal DBUtil As POSMySQL.POSControl.CDBUtil, ByVal objcnn As MySqlConnection) As Integer
        Dim rResult() As DataRow
        Dim strSQL As String
        Dim dtResult As DataTable
        strSQL = "Show Fields From PrintJobOrderLineDetailFront "
        dtResult = DBUtil.List(strSQL, objcnn)
        'Check PayType Field : UseFontType
        rResult = dtResult.Select("Field = 'UseFontType'")
        If rResult.Length = 0 Then
            strSQL = "ALTER TABLE PrintJobOrderLineDetail ADD UseFontType tinyint NOT NULL DEFAULT '0' After JobOrderLineType "
            DBUtil.sqlExecute(strSQL, objcnn)
            strSQL = "ALTER TABLE PrintJobOrderLineDetailFront ADD UseFontType tinyint NOT NULL DEFAULT '0' After JobOrderLineType "
            DBUtil.sqlExecute(strSQL, objcnn)
        End If
    End Function

    Public Shared Function GetStaffFromWorkingSession(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal shopID As Integer, _
    ByVal orderDate As String) As DataTable
        Dim strSQL As String
        strSQL = "Select s.StaffID, s.StaffCode, s.StaffFirstName, s.StaffLastName " & _
                 "From Staffs s, StaffWorkingSession sw " & _
                 "Where s.StaffID = sw.StaffID AND s.Deleted = 0 AND sw.SessionDate = " & orderDate & " AND sw.ProductLevelID = " & shopID & " AND " & _
                 " sw.EndTime IS NULL " & _
                 "Order By sw.StartTime, s.StaffCode, s.StaffFirstName "
        Return dbUtil.List(strSQL, objCnn)
    End Function

    Public Shared Function InsertPrintJobOrderRecordLog(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, _
    ByVal transID As Integer, ByVal transComID As Integer, ByVal groupOfPrintNo As String, ByVal printDateTime As String, _
    ByVal printFromComID As Integer) As Integer
        Dim strSQL As String
        Dim strTransID As String
        strTransID = "Where TransactionID = " & transID & " AND ComputerID = " & transComID
        If groupOfPrintNo <> "" Then
            strTransID &= " AND PrintNo IN (" & groupOfPrintNo & ")"
        End If
        strSQL = "INSERT INTO PrintJobOrderRecordLog(TransactionID, ComputerID, OrderDetailID, PrintNo, IsPrintSummary, " & _
                 "PrintDateTime, PrintFromComputerID) " & _
                 "Select TransactionID, ComputerID, OrderDetailID, PrintNo, IsPrintSummary, " & printDateTime & _
                 ", " & printFromComID & " " & _
                 "From PrintJobOrderDetailFront " & strTransID
        dbUtil.sqlExecute(strSQL, objCnn)
    End Function

    Public Shared Function GetProductForUpdateOutOfStockProduct(ByVal dbUtil As CDBUtil, ByVal objCnn As MySqlConnection, ByVal transID As Integer, _
     ByVal transComID As Integer, ByVal strOrderID As String) As DataTable
        Dim strSQL As String
        strSQL = "Select ProductID, ProductSetType, SaleMode, Sum(Amount) as TotalAmount " & _
                 "From OrderDetailFront " & _
                 "Where TransactionID = " & transID & " AND ComputerID = " & transComID & " AND " & _
                 " OrderDetailID IN (" & strOrderID & ") AND ProductID <> 0 " & _
                 "Group By ProductID, ProductSetType, SaleMode "
        Return dbUtil.List(strSQL, objCnn)
    End Function
End Class


















