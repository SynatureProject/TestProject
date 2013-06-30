
Public Class POSAdditionalProgramVariable

    Public Const SHOP_PRINTRECEIPTFORMAT As Integer = 1                 'Print Receipt Format
    Public Const SHOP_PRINTJOBORDERFORMAT As Integer = 2                'Print Job Order Format
    Public Const SHOP_PRINTSESSIONFORMAT As Integer = 3                 'Print Session Format
    Public Const SHOP_EXCLUDERECEIPTNO As Integer = 4                  'Exclude Receipt No when Print Receipt
    Public Const PROPERTY_PRINTDIFFAMOUNTINSESSION As Integer = 5      'Print Diff Amount when Open and Close Amount Session
    Public Const PROPERTY_STOCKINSALEREPORTFORDORO As Integer = 6      'Display Stock Sale Report For Doro

    Public Const PROPERTY_DEFAULTNOPRINTSESSION As Integer = 7         'Default No of Print Session in Session Report
    Public Const PROPERTY_LOCKNOPRINTSESSION As Integer = 8            'Lock No of Print Session in Session Report

    Public Const COMPUTER_DEFAULTTABLEZONE As Integer = 9              'Default Table Zone when load restaurant front

    Public Const SHOP_PRINTTOKITCHENBYTABLEZONE As Integer = 10         'Print To Kitchen by PrinterByTableZone --> Using StockToInvID From PrinterByTableZone
    Public Const SHOP_PRINTNOBOOKORDERINRECEIPT As Integer = 11         'Print No BookRecord In Receipt

    Public Const PROPERTY_HASINSERTCOUPONVOUCHERDIALOG As Integer = 12  'Has Insert Coupon/Voucher Input Window
    Public Const PROPERTY_COUPONVOUCHERDELIMETER As Integer = 13        'Delimeter/ no of digit in front part of Coupon/ Voucher

    Public Const PROPERTY_REPRINTALLTRANSACTIONINONEDAY As Integer = 14 'Reprint All Transaction In One Day
    Public Const SHOP_PRINTREDCOMMENTTOKITCHEN As Integer = 15          'Print Red Comment To Kitchen

    Public Const PROPERTY_PRINTTEXTAFTERTAKEAWAYPRODUCT As Integer = 16 'Print Text After Take Away Product Name In Receipt
    Public Const SHOP_PRINTPRODUCTNAMELANGIDINRECEIPT As Integer = 17   'Print Product Name Column In Receipt

    Public Const PROPERTY_NUMBERMONTHKEEPSLOTDATA As Integer = 18       'No of Month for Keep Slot Data In database
    Public Const SHOP_NOTDISPLAYOTHERSESSIONSUMMARY As Integer = 19      'Not display other session summary in current session

    Public Const SHOP_PRINTTABLEZONEINRECEIPT As Integer = 20              'Print TableZone In Receipt
    Public Const SHOP_NUMBERCOLUMNATPRODUCTDETAILINRECEIPT As Integer = 21 'No. column at productdetail section In Receipt

    Public Const SHOP_RECORDPRINTNO As Integer = 22                    'Record Print No by Print Group (Number of Paper print from kitchen) 
    Public Const SHOP_CASHOUTFEATURE As Integer = 23                   'CashOut Feature

    Public Const SHOP_SPLITORDERBYPRODUCTAMOUNT As Integer = 24        'Split Print Order by ProductAmount
    Public Const SHOP_PRINTPRICEINJOBORDER As Integer = 25             'Print Product Price To JobOrder
    Public Const SHOP_PRINTCOLUMNPRODUCTNAMEINJOBORDER As Integer = 26 'Print Column ProductName To JobOrder

    Public Const SHOP_TABLELISTORDERING As Integer = 27                 'Table List Ordering (For Restaurant)
    Public Const SHOP_CALCULATEVATBEFOREDISCOUNT As Integer = 28       'Calculate VAT Before Discount

    Public Const SHOP_RESETRECEIPTIDTYPE As Integer = 29               'Reset ReceiptID
    Public Const SHOP_HAS2RECEIPTACCOUNT As Integer = 30                'Has 2 Receipt Account

    Public Const SHOP_PRINTORDERPROCESSNO As Integer = 31               'Print ProcessNo or Barcode For OrderProcess No
    Public Const SHOP_PRINTVATABLEFORMATINRECEIPT As Integer = 32      'Print VATAble/VAT Format In Receipt

    Public Const PROPERTY_STAFFLOGINONECOMPUTERATONETIME As Integer = 33     'Check Staff can log in 1 computer at 1 time

    Public Const SHOP_ADDPRODUCTGROUPFOROTHERPRODUCT As Integer = 34     'Add ProductGroup For Other Product
    Public Const SHOP_SAVECREDITCARDWHENOPENTRANSACTION As Integer = 35     'Save CreditCard Detail When Open New Transaction

    ' PropertyID 37,38 For Clinic Version

    Public Const SHOP_NOTMOVEORDERPROCESSAFTERCLOSEBILL As Integer = 39     'Not Move OrderProcessDetailFront To OrderProcessDetail After CloseBill
    Public Const SHOP_CALCULATEREDEEMPOINT As Integer = 40                  'Calculate RedeemPoint When CheckBill

    Public Const PROPERTY_RECORDORDERTRANSACTIONWHENPRINTDETAIL As Integer = 41        'Record OrderTransaction When Print Receipt
    Public Const COMPUTER_AUTOIMPORTEXPORTDATA As Integer = 42                  'Auto Import/ Export Data At Front Feature
    Public Const COMPUTER_AUTOBACKUPANDCLEARDATA As Integer = 43                'Auto Backup/ Clear Data After Close Session

    Public Const SHOP_PRINTPRICESUMMARYWHENPRINTJOBORDER As Integer = 44       'Print Price Summary To Receipt When Print Job Order
    Public Const SHOP_MANUALSELECTORDERFORCALCULATEDISCOUNT As Integer = 45    'Manual Select Order For Calculate Discount
    Public Const SHOP_RECORDDATAINPROMOTIONDISCOUNTDETAIL As Integer = -45    'Record Data In PromotionDiscountDetail or VoucherDiscountDetail

    Public Const SHOP_ADDORDERAMOUNTBY1 As Integer = 46                        'Add Order Amount By 1
    Public Const SHOP_USEPOCKETPCPRODUCTNAME As Integer = 47                   'PocketPC Use Field ProductPocketName.
    Public Const SHOP_GROUPPRODUCTFORPRINTSUMMARYPRICEINJOBORDER As Integer = 48       'Group Same Product In Price Summary When Print JobOrder
    Public Const SHOP_PRINTCOLUMNPRODUCTNAMEFORSUMMARYPRICEINJOBORDER As Integer = 49  'Print Column ProductName In Price Summary When Print JobOrder
    ' PropertyID 50 For AutoNetLinkClient

    Public Const SHOP_PRINTTRANSACTIONHISTORYINRECEIPT As Integer = 51         'Print Transaction History Into Receipt (Such as Delete Order, Move Order)
    Public Const PROPERTY_PRODUCTFIXCOMMENT As Integer = 52                    'Fix Comment For Each Product
    Public Const PROPERTY_ENABLEUPSIZEPRODUCT As Integer = 53                  'Enable UpSize Product In Menu (Back Office)
    Public Const SHOP_SEARCHPRODUCTCOLUMNNAME As Integer = 54                  'Enable UpSize Product In Menu (Back Office)
    Public Const SHOP_SWITCHUSERANDAUTHORIZEDIALOGFORM As Integer = 55         'Switch User And Authorize Dialog Display Form
    Public Const SHOP_AFTERENDDAYFUNCTION As Integer = 56                      'Process After EndDay Function
    Public Const SHOP_AFTERSUBMITORDER As Integer = 57                         'Process After Submit Order
    Public Const SHOP_USECURRENTSTAFFPERMISSIONTOAUTHORIZE As Integer = 58     'Use Current Staff Permission Try To Authorize Delete/ Change Order First

    Public Const SHOP_PRINTCUSTOMERSUMMARYWHENPRINTJOBORDER As Integer = 59    'Print Customer Summary When Print Job Order (Same As Property 44)
    Public Const SHOP_AFTERSAVEORDER As Integer = 60                           'Process After Save Order
    Public Const SHOP_KDSSYSTEM As Integer = 61                                'Kitchen Display System 
    Public Const SHOP_REFERENCENOFORTRANSACTION As Integer = 62                'Reference No For Transaction

    Public Const SHOP_NOTPRINTFLEXIBLEHEADER As Integer = 63                   'Not Print Flexible Product Header When Print Flexible Product To Kitchen
    Public Const SHOP_PRINTOPENSESSIONAMOUNT As Integer = 64                   'Print Open Session Amount When Open New Session
    Public Const SHOP_DISABLEHOLDORDERFEATURE As Integer = 65                  'Disable Hold Order Feature
    Public Const SHOP_ALWAYSCHECKSERVICECHARGE As Integer = 66                 'Always Check Calculate ServiceCharge
    Public Const SHOP_OPENMEMBERDETAILAFTERSEARCHMEMBER As Integer = 67        'Open MemberDetail After Search Member
    Public Const SHOP_DEFAULTTABINMEMBERDETAIL As Integer = 68                 'Default Tab For MemberDetail
    Public Const SHOP_PRINTDELETEDORDERAFTERPRINTBILLDETAILINRECEIPT As Integer = 69    'Print Deleted Order After Print BillDetail In Receipt
    Public Const SHOP_PRINTNUMBEROFPRINTBILLDETAILINSESSION As Integer = 70             'Print Number of Print Bill Detail In Session
    Public Const SHOP_PRINTNUMBEROFPRINTBILLDETAILINRECEIPT As Integer = 71             'Print Number of Print Bill Detail In Receipt
    Public Const SHOP_PRINTREASONWHENREPRINTBILL As Integer = 72                        'Print Reason For RePrintBillDetail/ RePrintReceipt
    Public Const SHOP_SAVERECEIPTFOREJOURNAL As Integer = 73                            'Save Receipt Detail For Print E Journal
    Public Const SHOP_PRINTENDDAYFORM As Integer = 74                           'Print EndDay Report Form
    Public Const SHOP_EXPORTDATAFORKINGPOWER As Integer = 75                   'Export Data For King Power
    Public Const SHOP_PRODUCTCATEGORYCODEFORKINGPOWER As Integer = 76          'Product Category Code For King Power
    Public Const SHOP_NOTPRINTRECEIPTNUMBERINSESSION As Integer = 77           'Print Start-End Receipt Number In Session

    Public Const COMPUTER_BUFFERTPRINTWARNING As Integer = 78                   'Print Buffet Warning Time To Printer
    Public Const SHOP_NOTALLOWADDOTHERPRODUCT As Integer = 79                   'Not Allow To Add Other Product
    Public Const SHOP_RECORDNOCUSTOMERBYBUFFETPRODUCT As Integer = 80           'Record Number Customer By Buffet Product Amount
    Public Const SHOP_PRINTIDFORCANCELORMOVEORDER As Integer = 81               'PrinterID For Cancel/ Move Order
    Public Const SHOP_PRINTREFERENCERECEIPTNO As Integer = 82                   'Print Reference BillNo To Receipt
    Public Const SHOP_ADDPRODUCTFROMPACKAGEFEATURE As Integer = 83              'Add Product From Package Feature
    Public Const SHOP_DEPOSITITEMFEATURE As Integer = 84                        'Deposit Item Feature
    Public Const SHOP_SESSIONACCOUNTINCLUDECASHINOUT As Integer = 85            'Calculate Session Account Include CashIn/Out

    Public Const SHOP_ADDPRODUCTINPRODUCTSETINORDER As Integer = 86             'Add Product In Product Set In Order (For Version 5 --> Set TableID To Hold Transaction)
    Public Const SHOP_SAVEPRINTJOBORDERDETAIL As Integer = 87                   'Save PrintJob Order When Submit And Print At Print Manager
    Public Const SHOP_DISPLAYPRODUCTINPRODUCTSETPRICEATPARENT As Integer = 88   'Display Product In ProductSet Price At Parent

    Public Const SHOP_AFTERPRINTBILLDETAIL As Integer = 89                      'Process After Print Bill Detail
    Public Const PROPERTY_CHECKSTAFFACCESSWHENLOGIN As Integer = 90             'Check Staff Access Shop When Login
    Public Const PROPERTY_DISPLAYCOMMENTORDERINSALEBYPRODUCT As Integer = 91    'Display Comment Order In SaleByProduct/ Session Report
    Public Const PROPERTY_LOCKINPUTMEMBERBYKEYPAD As Integer = 92               'Lock Member Key Paid
    Public Const SHOP_PRODUCTGROUPCODE_DISPLAYBYSALEMODE_INSESSION As Integer = 93      'ProductGroupCode for Display By SaleMode In Session For Oishi
    Public Const SHOP_DEPOSITCASHFEATURE As Integer = 94                        'Deposit Cash Feature In Session

    Public Const SHOP_OUTOFSTOCKSETTING As Integer = 95                         'Out Of Stock Setting Type
    Public Const PROPERTY_SYSTEMFEATUREFOR As Integer = 96                      'System Type For Company Such as CRG
    Public Const SHOP_SESSIONENDDAYVALIDATION As Integer = 97                   'Session End Day Validate
    Public Const SHOP_DEFAULTSALEMODEFORPRODUCTCOMPONENT As Integer = 98        'Default SaleMode For ProductComponent
    Public Const SHOP_CHECKREALTIMESTOCKWHENADDPRODUCT As Integer = 99          'Check Real Time Stock When Add Product
    Public Const SHOP_SWITCHLANGUGEINMENU As Integer = 100                      'Switch ProductName In Menu At Front








    Public Const COMPUTER_TSCFEATURE As Integer = 1009                          'TSC Feature
    Public Const SHOP_TABLENAMEFORMERGETABLE As Integer = 1039                  'Display Table Name For Merge/Combine Table
    Public Const SHOP_24HOURSALE As Integer = 1049                              '24 Hr. Sale Feature




    '************** Full Tax Invoice Variable *******
    Public Const FULLTAX_SHOP_FULLTAXFORM As Integer = 1                           'FullTaxInvoice Form 0 for Default, >=1 for CrytalReport form
    Public Const FULLTAX_PROPERTY_PRINTCUSTOMERPACKAGE As Integer = 2              'Print Customer Package In FullTaxInvoice
    Public Const FULLTAX_SHOP_PRINTCOLUMNPRODUCTNAMEINFULLTAX As Integer = 3       'Print Column ProductName To FullTax
    Public Const FULLTAX_SHOP_SUMMARYORDERINTOONEWHNDPRINTFULLTAX As Integer = 4   'Print All Order In 1 Line 
    Public Const FULLTAX_SHOP_FULLTAXFROMMORETHAN1RECEIPT As Integer = 5           'Create FullTax Invoice From More than 1 Receipt

    Public Const FULLTAX_SHOP_FULLTAXADDRESSFROM As Integer = 6                     'Print FullTax Address Form
    Public Const FULLTAX_SHOP_NOTPRINTFLEXIBLEPRODUCT As Integer = 7                'Not Print Flexible Product In FullTax
    Public Const FULLTAX_SHOP_CREATERECEIPTIDTYPE As Integer = 8                    'Create ReceiptID



    '************** Inventory Variable *******
    Public Const INV_AUTOCREATROFORTRANSFERBAKERY As Integer = 1              'Auto create RO For Transfer Bakery
    Public Const INV_REQUESTDATEAFTERTODAY As Integer = 2                     'Request Date >= Today
    Public Const INV_TEMPLATEFEATURE As Integer = 3                           'Document Template Feature

    Public Const INV_PRINTPREVIEWFORM As Integer = 4                          'PrintPreview Form (0 = Default Form, 1 = Crytal Report)
    Public Const INV_LIMITINVENTORYVIEWFORREQUEST As Integer = 5              'Limit InventoryView For Request Document.
    Public Const INV_LIMITVENDORGROUPFORPO As Integer = 6                     'Limit Select VendorGroup For PO/DRO

    Public Const INV_WEEKLYSTOCKCOUNT As Integer = 7                          'Weeekly Stock Count Day
    Public Const INV_DOCUMENTTYPE_USEAVGPRICE_WHENAPPROVE As Integer = 8      'DocumentType Using Avg. Price When Approve
    Public Const INV_PREFINISHFEATURE As Integer = 9                          'Prefinish Feature

    Public Const INV_STOCKCARDMOVEMENTINOUTDISPLAYRECEIPT As Integer = 10     'Display Sale Receipt For Stockcard Movement In-Out
    Public Const INV_ROTRANSFERFORM As Integer = 11                           'Ro From Transfer Is Normal Form/ Compare Form
    Public Const INV_CANEDITAMOUNTWHENINSERTMATERIAL As Integer = 12          'Can Edit Amount When Insert Material

    Public Const INV_DISPLAYSTOCKCARDTYPE As Integer = 13                       'Display StockCard Type
    Public Const INV_ORDERINVENTORYBY As Integer = 14                           'Order Inventory Name In Combo By

    '************** BackOffice Variable *******
    Public Const BACKOFFICE_RESOURCEFOLDERFORTABLET As Integer = 9              'Resource Folder For Tablet




End Class





















