
Public Class POSType

    Public Const TRANSACTION_OPENBILL As Integer = 1
    Public Const TRANSACTION_CLOSEBILL As Integer = 2
    Public Const TRANSACTION_RESERVE As Integer = 3
    Public Const TRANSACTION_CANCELRESERVE As Integer = 4
    Public Const TRANSACTION_VOIDALL As Integer = 5
    Public Const TRANSACTION_VOIDALLNOPRODUCE As Integer = 8
    Public Const TRANSACTION_FREE As Integer = 6
    Public Const TRANSACTION_CONFIRMREESRVE As Integer = 10
    Public Const TRANSACTION_COMBINEBILL As Integer = 7
    Public Const TRANSACTION_HOLDBILL As Integer = 9
    Public Const TRANSACTION_BILLOTHERRECEIPTHEADER As Integer = 11
    Public Const TRANSACTION_VOIDOTHERRECEIPTHEADERALL As Integer = 12
    Public Const TRANSACTION_VOIDOTHERRECEIPTHEADERALLNOTPRODUCE As Integer = 13
    Public Const TRANSACTION_AUTOCANCELRESERVE As Integer = 14
    Public Const TRANSACTION_SPLITTRANSACTION As Integer = 15

    Public Const TRANSACTION_TRANSACTIONNOTFOUND As Integer = -100

    Public Const ORDERSTATUS_SUBMIT As Integer = 1
    Public Const ORDERSTATUS_NOTSUBMIT As Integer = 2
    Public Const ORDERSTATUS_VOID As Integer = 3
    Public Const ORDERSTATUS_VOIDNOPRODUCE As Integer = 4
    Public Const ORDERSTATUS_PROMOTIONPRODUCT As Integer = 5
    Public Const ORDERSTATUS_INPROCESS As Integer = 6
    Public Const ORDERSTATUS_NOTCOMFIRM_BONUSPROUCT As Integer = 7
    Public Const ORDERSTATUS_INPROCESS_BONUSPRODUCT As Integer = 8
    Public Const ORDERSTATUS_FREEPRODUCTFORBACKRETAIL As Integer = 9

    Public Const ORDERPROCESSSTATUS_SUBMIT As Integer = 0
    Public Const ORDERPROCESSSTATUS_FINISH As Integer = 1
    Public Const ORDERPROCESSSTATUS_VOID As Integer = 3
    Public Const ORDERPROCESSSTATUS_VOIDNOTPRODUCE As Integer = 4

    Public Const SALEMODE_DINEIN As Integer = 1
    Public Const SALEMODE_TAKEAWAY As Integer = 2
    Public Const SALEMODE_DELIVERY As Integer = 3
    Public Const SALEMODE_DEPOSIT As Integer = 4

    Public Const SALEMODE_FORCREATEBILLDETAIL_REFERENCENO As Integer = 90


    Public Const QUEUECUSTOMER_WAITING As Integer = 1
    Public Const QUEUECUSTOMER_CHECKIN As Integer = 2
    Public Const QUEUECUSTOMER_CANCEL As Integer = 99

    Public Const KDS_INPROCESS As Integer = 1
    Public Const KDS_FINISH As Integer = 2
    Public Const KDS_PICKUP As Integer = 3
    Public Const KDS_CANCEL As Integer = 99


    Public Const VATTYPE_NONE As Integer = 0
    Public Const VATTYPE_INCLUDE As Integer = 1
    Public Const VATTYPE_EXCLUDE As Integer = 2

    Public Const PRODUCTTYPE_NORMALPRODUCT As Integer = 0
    Public Const PRODUCTTYPE_PRODUCTSET As Integer = 1
    Public Const PRODUCTTYPE_PRODUCTSIZE As Integer = 2
    Public Const PRODUCTTYPE_SPAPRODUCT As Integer = 3
    Public Const PRODUCTTYPE_PACKAGEPRODUCT As Integer = 4
    Public Const PRODUCTTYPE_AMOUNTEDPRODUCT As Integer = 5
    Public Const PRODUCTTYPE_FLEXIBLEPRODUCTSET As Integer = 6
    Public Const PRODUCTTYPE_GROUP_OF_FLEXIBLEPRODUCTSET As Integer = 7
    Public Const PRODUCTTYPE_GROUP_OF_SALON As Integer = 8
    Public Const PRODUCTTYPE_SALON As Integer = 9
    Public Const PRODUCTTYPE_PREPAID As Integer = 10
    Public Const PRODUCTTYPE_SMARTCARD As Integer = 11
    Public Const PRODUCTTYPE_SPA_MORETHAN1STAFF As Integer = 12
    Public Const PRODUCTTYPE_SALONPACKAGE As Integer = 13
    Public Const PRODUCTTYPE_COMMENT As Integer = 14
    Public Const PRODUCTTYPE_COMMENTWITHPRICE As Integer = 15
    Public Const PRODUCTTYPE_TRANSACTIONCOMMENT As Integer = 16
    Public Const PRODUCTTYPE_SESSIONPACKAGE As Integer = 17
    Public Const PRODUCTTYPE_PREPAIDPACKAGE As Integer = 18

    Public Const PRODUCTTYPE_PRODUCT_IN_PRODUCTSET As Integer = -1
    Public Const PRODUCTTYPE_PRODUCTSET_IN_PACKAGE As Integer = -2
    Public Const PRODUCTTYPE_PRODUCT_IN_PRODUCTSET_INPACKAGE As Integer = -3
    Public Const PRODUCTTYPE_PRODUCT_IN_PACKAGE As Integer = -4
    Public Const PRODUCTTYPE_PRODUCT_IN_FLEXIBLEPRODUCTSET As Integer = -6
    Public Const PRODUCTTYPE_SALONPRODUCT_IN_PACKAGE As Integer = -13
    Public Const PRODUCTTYPE_PRODUCTSET_IN_SESSIONPACKAGE As Integer = -15
    Public Const PRODUCTTYPE_PRODUCT_IN_PRODUCTSET_SESSIONPACKAGE As Integer = -16
    Public Const PRODUCTTYPE_PRODUCT_IN_SESSIONPACKAGE As Integer = -17




    Public Const FRONTFUNCTION_DELETESUBMITORDER As Integer = 1
    Public Const FRONTFUNCTION_EDITESUBMITORDER As Integer = 2
    Public Const FRONTFUNCTION_PRINTBILLDETAIL As Integer = 3
    Public Const FRONTFUNCTION_PRINTRECEIPT As Integer = 4
    Public Const FRONTFUNCTION_REPRINTBILL As Integer = 5
    Public Const FRONTFUNCTION_DELETENORMALORDER As Integer = 6
    Public Const FRONTFUNCTION_VOIDORDER As Integer = 7
    Public Const FRONTFUNCTION_CHANGETABLE As Integer = 8
    Public Const FRONTFUNCTION_COMBINETABLE As Integer = 9
    Public Const FRONTFUNCTION_MOVEORDERBETWEENTRANSACTION As Integer = 10
    Public Const FRONTFUNCTION_CHANGETABLEAFTERPRINTBILLDETAIL As Integer = 11
    Public Const FRONTFUNCTION_COMBINETABLEAFTERPRINTBILLDETAIL As Integer = 12
    Public Const FRONTFUNCTION_MOVEORDERBETWEENTRANSACTIONAFTERPRINTBILLDETAIL As Integer = 13
    Public Const FRONTFUNCTION_REPRINTORDER As Integer = 14
    Public Const FRONTFUNCTION_REPRINTORDERFROMPOCKET As Integer = 15
    Public Const FRONTFUNCTION_PRINTRETURNORDER As Integer = 16
    Public Const FRONTFUNCTION_PRINTVOIDORDER As Integer = 17
    Public Const FRONTFUNCTION_PRINTEDITORDER As Integer = 18
    Public Const FRONTFUNCTION_MANUALOPENCASHDRAWER As Integer = 19
    Public Const FRONTFUNCTION_PRINTFULLTAXINVOICE As Integer = 20
    Public Const FRONTFUNCTION_REPRINTFULLTAXINVOICE As Integer = 21
    Public Const FRONTFUNCTION_VOIDTRANSACTION As Integer = 22
    Public Const FRONTFUNCTION_APPLIEDPROMOTION As Integer = 25
    Public Const FRONTFUNCTION_REMOVEPROMOTION As Integer = 26
    Public Const FRONTFUNCTION_DELETESUBMITORDERAFTERPRINTBILLDETAIL As Integer = 27
    Public Const FRONTFUNCTION_VOIDORDERAFTERPRINTBILLDETAIL As Integer = 28
    Public Const FRONTFUNCTION_COPYORDERFROMVOIDTRANSACTION As Integer = 29
    Public Const FRONTFUNCTION_VOIDFULLTAXINVOICE As Integer = 30
    Public Const FRONTFUNCTION_MOVEORDER_SPLITTRANSACTION As Integer = 31
    Public Const FRONTFUNCTION_VOIDREDEEM As Integer = 32
    Public Const FRONTFUNCTION_SPLITTABLEFROMCOMBINE As Integer = 33
    Public Const FRONTFUNCTION_SPLITTABLEFROMCOMBINEAFTERPRINTBILL As Integer = 34
    Public Const FRONTFUNCTION_CHANGEPRODUCT As Integer = 35

    Public Const REASONTEXTGROUP_NONE As Integer = -1
    Public Const REASONTEXTGROUP_VOIDTRANSACTION As Integer = 1
    Public Const REASONTEXTGROUP_VOIDORDER As Integer = 2
    Public Const REASONTEXTGROUP_REPRINTBILL As Integer = 3
    Public Const REASONTEXTGROUP_REPRINTORDER As Integer = 4
    Public Const REASONTEXTGROUP_CHANGETABLE As Integer = 5
    Public Const REASONTEXTGROUP_COMBINETABLE As Integer = 6
    Public Const REASONTEXTGROUP_MOVEORDER As Integer = 7
    Public Const REASONTEXTGROUP_MANUALOPENDRAWER As Integer = 8
    Public Const REASONTEXTGROUP_CHANGEPRODUCT As Integer = 9


    Public Const RECEIPTTYPE_RECEIPT_HEADER As Integer = 0
    Public Const RECEIPTTYPE_RECEIPT_FOOTER As Integer = 1
    Public Const RECEIPTTYPE_FULLTAX_HEADER As Integer = 2
    Public Const RECEIPTTYPE_BANKRETAIL_HEADER As Integer = 3
    Public Const RECEIPTTYPE_BILLDETAIL_HEADER As Integer = 4
    Public Const RECEIPTTYPE_BILLDETAIL_FOOTER As Integer = 5
    Public Const RECEIPTTYPE_FULLTAX_FOODER As Integer = 6
    Public Const RECEIPTTYPE_JOBORDERSUMMARYPRICE_FOOTER As Integer = 7
    Public Const RECEIPTTYPE_ENDDAY_HEADER As Integer = 8
    Public Const RECEIPTTYPE_ENDSHIFT_HEADER As Integer = 9
    Public Const RECEIPTTYPE_QUEUECUSTOMER_FOOTER As Integer = 10
    Public Const RECEIPTTYPE_DEPOSIT_HEADER As Integer = 11
    Public Const RECEIPTTYPE_DEPOSIT_FOOTER As Integer = 12
    Public Const RECEIPTTYPE_ITEMDEPOSIT_HEADER As Integer = 13
    Public Const RECEIPTTYPE_ITEMDEPOSIT_FOOTER As Integer = 14
    Public Const RECEIPTTYPE_CASHOUT_HEADER As Integer = 15
    Public Const RECEIPTTYPE_CASHOUT_FOOTER As Integer = 16
    Public Const RECEIPTTYPE_QUEUECUSTOMER_HEADER As Integer = 17
    Public Const RECEIPTTYPE_PRINTSIGNATURE_FOOTER As Integer = 18
    Public Const RECEIPTTYPE_VOIDORDER_FOOTER As Integer = 19
    Public Const RECEIPTTYPE_VOIDTRANSACTION_FOOTER As Integer = 20

    Public Const RECEIPTTYPE_OTHERRECEIPT_HEADER As Integer = 21
    Public Const RECEIPTTYPE_OTHERRECEIPT_FOOTER As Integer = 22

    Public Const RECEIPTTYPE_SESSIONAMOUNT_HEADER As Integer = 23
    Public Const RECEIPTTYPE_SESSIONAMOUNT_FOOTER As Integer = 24

    Public Const RECEIPTTYPE_MOVEORDER_HEADER As Integer = 25
    Public Const RECEIPTTYPE_MOVEORDER_FOOTER As Integer = 26
    Public Const RECEIPTTYPE_VOIDORDER_HEADER As Integer = 27

    Public Const RECEIPTTYPE_ENDDAY_FOOTER As Integer = 28
    Public Const RECEIPTTYPE_ENDSHIFT_FOOTER As Integer = 29






    Public Const PROMOTION_HEADERFOOTER_NEXTCOUPON_HEADER As Integer = 0
    Public Const PROMOTION_HEADERFOOTER_NEXTCOUPON_FOOTER As Integer = 1


    Public Const BOOKRECORD_NONE As Integer = 0
    Public Const BOOKRECORD_FROMDIALOG As Integer = 1
    Public Const BOOKRECORD_NOOFSUBMIT As Integer = 2

    Public Const PROGRAMTYPE_FRONT As Integer = 1
    Public Const PROGRAMTYPE_INVENTORY As Integer = 3
    Public Const PROGRAMTYPE_FULLTAXUTIL As Integer = 4
    Public Const PROGRAMTYPE_SPARESERVATION As Integer = 5
    Public Const PROGRAMTYPE_CHECKER As Integer = 6


    Public Const ViewPayType_AllAvailablePayment As Integer = 1
    Public Const ViewPayType_OnlyNormalPayment As Integer = 2
    Public Const ViewPayType_OnlyOtherReceiptPayment As Integer = 3

    Public Const PayByCash As Integer = 1
    Public Const PayByCreditCard As Integer = 2
    Public Const PayBySmartcard As Integer = 3
    Public Const PayByCheque As Integer = 4
    Public Const PayByCreditMoney As Integer = 5
    Public Const PayByRedeemPoint As Integer = 6
    Public Const PayByMoneyCoupon As Integer = 7
    Public Const PayByMultiplePayment As Integer = 8
    Public Const PayByPrepaidBarcode As Integer = 9
    Public Const PayByOtherReceiptHeader As Integer = 10
    Public Const PayByOtherPaymentType As Integer = 10
    Public Const PayByDeposit As Integer = 1004

    Public Const PayByCreditCardEDC1 As Integer = 1001
    Public Const PayByCreditCardEDC2 As Integer = 1002
    Public Const PayByCreditCardEDC3 As Integer = 1003

    Public Const EDCType_Nora As Integer = 1
    Public Const EDCType_PosNet As Integer = 2
    Public Const EDCType_ManualPosNet As Integer = 102

    Public Const DISCOUNTTYPE_PRICE As Integer = 2
    Public Const DISCOUNTTYPE_MEMBER As Integer = 1
    Public Const DISCOUNTTYPE_STAFF As Integer = 3
    Public Const DISCOUNTTYPE_COUPON As Integer = 4
    Public Const DISCOUNTTYPE_VOUCHER As Integer = 5
    Public Const DISCOUNTTYPE_OTHER As Integer = 6
    Public Const DISCOUNTTYPE_SUBPROMOTION As Integer = 7

    Public Const PRINTBILLDETAIL_NOPRINT As Integer = 0
    Public Const PRINTBILLDETAIL_PRINT As Integer = 1
    Public Const PRINTBILLDETAIL_PRINTWITHAUTHORIZE As Integer = 2
    Public Const PRINTBILLDETAIL_DISPLAYPREVIEW As Integer = 3
    Public Const PRINTBILLDETAIL_ALWAYSPRINTWITHAUTHORIZE As Integer = 4

    Public Const AFTERSUBMITORDER_DONTHING As Integer = 0
    Public Const AFTERSUBMITORDER_BACKTOMAINSCREEN As Integer = 1
    Public Const AFTERSUBMITORDER_TOSWICHUSERDIALOG As Integer = 2

    Public Const LAYOUTTABLESTATUS_EMPTY As Integer = 0
    Public Const LAYOUTTABLESTATUS_OCCUPIED As Integer = 1
    Public Const LAYOUTTABLESTATUS_RESERVE As Integer = 2
    Public Const LAYOUTTABLESTATUS_PAID As Integer = 3
    Public Const LAYOUTTABLESTATUS_HASORDER As Integer = 4
    Public Const LAYOUTTABLESTATUS_PRINTBILLDETAIL As Integer = 5

    Public Const UDDCONTROL_TEXT As Integer = 1
    Public Const UDDCONTROL_INTEGERVALUE As Integer = 2
    Public Const UDDCONTROL_CHECKBOX As Integer = 3
    Public Const UDDCONTROL_RADIOOPTION As Integer = 4
    Public Const UDDCONTROL_DATE As Integer = 5

    Public Const CASHINOUTTYPE_CASHOUT As Integer = 1
    Public Const CASHINOUTTYPE_CASHIN As Integer = 2
    Public Const CASHINTOUTYPE_PETTYCASHOUT As Integer = 3
    Public Const CASHINOUTTYPE_PETTYCASHIN As Integer = 4
    Public Const CASHINOUTTYPE_DEPOSITCASH As Integer = 5

    Public Const SESSIONACCOUNTADJUSTTYPE_FROMVOID As Integer = 1


    Public Const DEPOSITSTATUS_DEPOSIT As Integer = 1
    Public Const DEPOSITSTATUS_FINISH As Integer = 2
    Public Const DEPOSITSTATUS_LINKTOTRANSACTION As Integer = 3
    Public Const DEPOSITSTATUS_CANCEL As Integer = 4

    Public Const RECEIPT_NORMAL As Integer = 8
    Public Const RECEIPT_FULLTAX As Integer = 11
    Public Const RECEIPT_CREDITMONEY As Integer = 33
    Public Const RECEIPT_CASHINOUT As Integer = 9
    Public Const RECEIPT_DEPOSIT As Integer = 61
    Public Const RECEIPT_CASHRECEIPT As Integer = 4


    Public Const CHECKER_NOCHECKER As Integer = 0
    Public Const CHECKER_MOVEPROCESSATPAYMENT As Integer = 1
    Public Const CHECKER_MOVEPROCESSATENDDAY As Integer = 2

    Public Const BUFFET_NOBUFFET As Integer = 0
    Public Const BUFFET_BEGINTIME_AT_OPENTABLE As Integer = 1
    Public Const BUFFET_BEGINTIME_AT_FIRST_FINISHORDER As Integer = 2
    Public Const BUFFET_BEGINTIME_AT_FIRST_SUBMITORDER As Integer = 3

    Public Const CALLFORCHECKBILL_NOTCALL As Integer = 0
    Public Const CALLFORCHECKBILL_CALL As Integer = 1
    Public Const CALLFORCHECKBILL_ALREADYPROCESS As Integer = 99

    Public Const JOBORDERSTATUS_NOTADD As Integer = 0
    Public Const JOBORDERSTATUS_WAITFORPRINT As Integer = 1
    Public Const JOBORDERSTATUS_PRINTSUCCESS As Integer = 2
    Public Const JOBORDERSTATUS_CONNECTPRINTERFAIL As Integer = 3

    Public Const JOBORDERSTATUS_OTHERERROR As Integer = 99


    Public Const ISCALSERCHARGE_NOTCALSERCHARGE_CALEXCLUDEVAT As Integer = 0
    Public Const ISCALSERCHARGE_CALSERCHARGE_CALEXCLUDEVAT As Integer = 1
    Public Const ISCALSERCHARGE_CALSERCHARGE_NOTCALEXCLUDEVAT As Integer = 2
    Public Const ISCALSERCHARGE_NOTCALSERCHARGE_NOTCALEXCLUDEVAT As Integer = 3


    Public Const EPRINTRECEIPTTYPE_NORMAL As Integer = 0
    Public Const EPRINTRECEIPTTYPE_REPRINT As Integer = 1
    Public Const EPRINTRECEIPTTYPE_VOIDRECEIPT As Integer = 2
    Public Const EPRINTRECEIPTTYPE_FULLTAXINVOICE As Integer = 3
    Public Const EPRINTRECEIPTTYPE_VOIDFULLTAXINVOICE As Integer = 4
    Public Const EPRINTRECEIPTTYPE_CANCELRECEIPTFORNEWFULLTAX As Integer = 5

    Public Const REDEEMTYPE_PRODUCT As Integer = 1
    Public Const REDEEMTYPE_COUPONVOUCHER As Integer = 2
    Public Const REDEEMTYPE_PAYMENT As Integer = 3

    Public Const ISCREATEVOUCHERCOUPON_PRODUCT As Integer = 0
    Public Const ISCREATEVOUCHERCOUPON_COUPONVOUCHER As Integer = 1
    Public Const ISCREATEVOUCHERCOUPON_PAYMENT As Integer = 2

    Public Const PICKTRANSACTION_NONE As Integer = 0
    Public Const PICKTRANSACTION_UPDATERECEIPTTOORDERTRANSACTION As Integer = 1
    Public Const PICKTRANSACTION_SAVERECEIPTINPICKTRANSACTION As Integer = 2
    Public Const PICKTRANSACTION_MANUALSELECTPICKTRANSACTION As Integer = 3

    Public Const RETURNORDERTYPE_NONE As Integer = 0
    Public Const RETURNORDERTYPE_RETURNORDER As Integer = 1
    Public Const RETURNORDERTYPE_CHANGEPRODUCT As Integer = 2

    Public Const RECEIPT_PARAMETERWORD_REGISTERNUMBER As String = "#POS_NUM#"
    Public Const RECEIPT_PARAMETERWORD_SHOPNAME As String = "#POS_SHOP#"

    Public Const RESETRECEIPT_DEFAULT_YEARMONTH As Integer = 0
    Public Const RESETRECEIPT_BYDAY As Integer = 1
    Public Const RESETRECEIPT_BYYEAR As Integer = 2



End Class


Public Enum ShowTable
    AllTable
    EmptyTable
    NotEmptyTable
    OccupiedTable
    ReserveTable
    PaidTable
    PrintBillDetailTable
    CallForCheckBill
End Enum

Public Enum TableStatus
    Empty
    Occupied
    Reserved
    Paid
    OccupiedNoOrder
    PrintBillDetail
    WarningTime
    OverTime
    CallForCheckBill
End Enum

Public Enum SpaStatus
    HasOrder
    NoOrder
    CloseBill
    CompleteSpa
End Enum

Public Enum OrderPrintStatus
    NoPrintBeforeSubmit
    NoPrintAfterSubmit
    NotPrintYet
    Print
End Enum

Public Enum TableImage
    ImageEmpty
    ImageOccupied
    ImageReserved
    ImagePaid
    ImageOccupiedNoOrder
    ImagePrintBillDetail
End Enum

Public Enum DiscountType
    MemberStaffDiscount
    MemberDiscount
    StaffDiscount
    CouponDiscount
    OtherDiscount
    EachProductDiscount
    VoucherDiscount
    PricePromotionDiscount
    VoucherCouponDiscount
End Enum

Public Enum RoundTo
    RoundUp
    RoundDown
End Enum

Public Enum RoundType
    None
    ZeroOne
    Point5
    Point25
    ZeroOneDown
    Point5Down
    Point25Down
    ZeroOneByRoundingFunction
    Point5ByRoundingFunction
    Point25ByRoundingFunction
End Enum

Public Enum ServiceChargeType
    CalculateBeforeAllDiscount
    CalculateAfterAllDiscount
End Enum

Public Enum PaymentBy
    ByCash
    ByCreditCard
    BySmartcard
    ByCheque
    ByCreditMoney
    ByRunAway
    ByMoneyCoupon
    ByMultiplePayment
    ByPrepaidBarcode
    ByOtherReceiptHeader
    ByDeposit
    ByOtherPaymentType
End Enum

Public Enum OtherDiscountType
    ByAmount
    ByPercent
    EachProduct
End Enum

Public Enum MemberStaffDiscountType
    MemberDiscount
    StaffDiscount
End Enum

Public Enum ManageOrderOperation
    AddProduct
    SearchProduct
    AddOtherProduct
    EditProduct
    DeleteProduct
    ChageOrderSaleMode
    ChangeTable
    MoveOrder
    CombineTable
    SplitTableFromCombine
    HoldOrder
    ShowHoldOrder
    CheckBill
    VoidBill
    AddProductFromPackage
    CompleteInProcessSpaProduct
    RePrintOrder
    PrintBillDetail
    AddOrderComment
    AddTransactionComment
    ReturnProduct
    OutOfStockProduct
    HotPayByCash
    Payment
    Discount
    ChangeLanguage
    SwitchTable
    DisplayMemberDetail
    SubmitPrintOrder
    FrontUtility
    SubmitPayment
    EditCustomer
    SetSplitTransaction
    DiscountSplitTransaction
    BackToMain
End Enum

Public Enum paymentDetailLineType
    SubTotal
    UnSubmitProductPrice
    MemberStaffDiscount
    CouponDiscount
    VoucherDiscount
    OtherDiscount
    ServiceCharge
    ExcludeVAT
    CardDetailAmount
    ChangeAmount
    PromotionDiscount
End Enum

Public Enum ShopType
    NotDefine
    Restaurant
    FastfoodRetail
    Spa
End Enum

Public Enum DataBaseType
    SQLServer
    MySQL
End Enum

Public Enum ProgramFunction
    FullFunction
    OnlyOrder
    OnlyPayment
End Enum

Public Enum CalculateDiscountProductBy
    Unknown
    FromMainPrice
    FromPromotionPrice
End Enum

Public Enum CalculateVATWhenFreeBill
    NoCalculate
    CalculateVAT
    CalculateFromPromotionPriceGroup
End Enum

Public Enum Gender
    Male
    Female
    Unknown
End Enum

Public Enum DepositCashFor
    ForNormalDeposit
    ForCloseSession
    ForCloseYesterdaySession
End Enum

Public Enum OutOfStockProduct
    NormalSetting
    CountDownSetting
End Enum

Public Enum MainSystemForCompany
    Normal
    CRG_Group
End Enum







