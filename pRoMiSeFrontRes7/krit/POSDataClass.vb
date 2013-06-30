Imports System.Drawing

Public Class HeaderLogo_Data
    Public SaleMode As Integer
    Public HeaderLogo As Image
    Public HeaderLogoPath As String
    Public HeaderLogoFileName As String

    Public Sub New()
        SaleMode = POSType.SALEMODE_DINEIN
        HeaderLogo = Nothing
        HeaderLogoFileName = ""
        HeaderLogoPath = ""
    End Sub

    Public Shared Function NewHeaderLogoData(ByVal saleMode As Integer, ByVal logoImage As Image, ByVal logoPath As String, _
    ByVal logoFileName As String) As HeaderLogo_Data
        Dim hData As New HeaderLogo_Data
        hData.SaleMode = saleMode
        hData.HeaderLogo = logoImage
        hData.HeaderLogoPath = logoPath
        hData.HeaderLogoFileName = logoFileName
        Return hData
    End Function

    Public Shared Function CopyNewLogoForSaleMode(ByVal saleMode As Integer, ByVal originalData As HeaderLogo_Data) As HeaderLogo_Data
        Dim hData As New HeaderLogo_Data
        hData.SaleMode = saleMode
        hData.HeaderLogo = originalData.HeaderLogo
        hData.HeaderLogoPath = originalData.HeaderLogoPath
        hData.HeaderLogoFileName = originalData.HeaderLogoFileName
        Return hData
    End Function


End Class

Public Class PrintReceiptOrder_Data
    Public LineType As Integer
    Public FontType As Integer
    Public LeftText As String
    Public CenterText As String
    Public RightText As String
    Public NameText As String
    Public IsUnderLine As Boolean
    Public DefaultFont As Font

    Public StartPosition As Decimal
    Public EndPosition As Decimal
    Public NoNewLineAfterPrint As NewLineAfterPrintType

    Public Enum NewLineAfterPrintType
        NewLine
        NoNewLine
        NewCopy
    End Enum

    Public Const RECEIPTLINETYPE_PRINTLEFT As Integer = 1
    Public Const RECEIPTLINETYPE_PRINTRIGHT As Integer = 2
    Public Const RECEIPTLINETYPE_PRINTCENTER As Integer = 3
    Public Const RECEIPTLINETYPE_PRINT3COLUMN As Integer = 4
    Public Const RECEIPTLINETYPE_PRINTLEFTRIGHT As Integer = 5
    Public Const RECEIPTLINETYPE_BLANKLINE As Integer = 6
    Public Const RECEIPTLINETYPE_PRINTDOUBLELINE As Integer = 7
    Public Const RECEIPTLINETYPE_PRINTLINE As Integer = 8
    Public Const RECEIPTLINETYPE_PRINTACROSSPAGE As Integer = 9
    Public Const RECEIPTLINETYPE_PRINTLOGO As Integer = 10
    Public Const RECEIPTLINETYPE_PRINT4COLUMN As Integer = 11
    Public Const RECEIPTLINETYPE_PRINT4COLUMNAMOUNTFIRST As Integer = 12
    Public Const RECEIPTLINETYPE_PRINT3COLUMNAMOUNTFIRST As Integer = 13
    Public Const RECEIPTLINETYPE_PRINTBARCODE As Integer = 14
    Public Const RECEIPTLINETYPE_PRINTQRCODE As Integer = 15
    Public Const RECEIPTLINETYPE_PRINTLEFTRIGHTINMIDDLE As Integer = 16
    Public Const RECEIPTLINETYPE_PRINTIMAGE_FROMLOCATION As Integer = 17
    Public Const RECEIPTLINETYPE_PRINTLEFTRIGHTACROSSPAGE As Integer = 18


    Public Const RECEIPTFONTTYPE_USEDEFAULTFONT As Integer = 0
    Public Const RECEIPTFONTTYPE_FIRSTLINEHEADER As Integer = 1
    Public Const RECEIPTFONTTYPE_OTHERLINEHEADER As Integer = 2
    Public Const RECEIPTFONTTYPE_FOOTER As Integer = 3
    Public Const RECEIPTFONTTYPE_RECEIPTHEADERDETAIL As Integer = 4
    Public Const RECEIPTFONTTYPE_PRODUCTANDPAYMENT As Integer = 5
    Public Const RECEIPTFONTTYPE_TABLENO As Integer = 6
    Public Const RECEIPTFONTTYPE_TOTALPRICE As Integer = 7
    Public Const RECEIPTFONTTYPE_CASHCHANGE As Integer = 8
    Public Const RECEIPTFONTTYPE_TIMEINOUT As Integer = 9
    Public Const RECEIPTFONTTYPE_TIMEINOUTATFOOTER As Integer = 10


    Public Const FULLTAXFONTTYPE_COMPANYNAME As Integer = 0
    Public Const FULLTAXFONTTYPE_COMPANYADDRESS As Integer = 1
    Public Const FULLTAXFONTTYPE_HEADER As Integer = 2
    Public Const FULLTAXFONTTYPE_FOOTER As Integer = 3
    Public Const FULLTAXFONTTYPE_FULLTAXCOLUMNNAME As Integer = 4
    Public Const FULLTAXFONTTYPE_FULLTAXDETAIL As Integer = 5
    Public Const FULLTAXFONTTYPE_ORDERCOLUMN As Integer = 6
    Public Const FULLTAXFONTTYPE_ORDERDETAIL As Integer = 7
    Public Const FULLTAXFONTTYPE_SUMMARYNAME As Integer = 8
    Public Const FULLTAXFONTTYPE_SUMMARYDETAIL As Integer = 9
    Public Const FULLTAXFONTTYPE_ADDITIONALFOOTER As Integer = 10
    Public Const FULLTAXFONTTYPE_ALPHABETSUMMARY As Integer = 11

    Public Overloads Shared Function NewReceiptData(ByVal lineType As Integer, ByVal fontType As Integer, ByVal leftText As String, ByVal centerText As String, _
    ByVal rightText As String, ByVal nameText As String, ByVal isUnderLine As Boolean, ByVal startPos As Decimal, ByVal endPos As Decimal, _
    ByVal noNewLineAfterPrint As Integer) As PrintReceiptOrder_Data
        Dim printReceipt As New PrintReceiptOrder_Data
        printReceipt.LineType = lineType
        printReceipt.FontType = fontType
        printReceipt.LeftText = leftText
        printReceipt.CenterText = centerText
        printReceipt.RightText = rightText
        printReceipt.NameText = nameText
        printReceipt.IsUnderLine = isUnderLine
        printReceipt.DefaultFont = Nothing
        printReceipt.StartPosition = startPos
        printReceipt.EndPosition = endPos
        Select Case noNewLineAfterPrint
            Case 0
                printReceipt.NoNewLineAfterPrint = NewLineAfterPrintType.NewLine
            Case 1
                printReceipt.NoNewLineAfterPrint = NewLineAfterPrintType.NoNewLine
            Case 2
                printReceipt.NoNewLineAfterPrint = NewLineAfterPrintType.NewCopy
            Case Else
                printReceipt.NoNewLineAfterPrint = NewLineAfterPrintType.NewLine
        End Select

        Return printReceipt
    End Function

    Public Overloads Shared Function NewReceiptData(ByVal lineType As Integer, ByVal fontType As Integer, ByVal leftText As String, ByVal centerText As String, _
    ByVal rightText As String, ByVal nameText As String, ByVal isUnderLine As Boolean) As PrintReceiptOrder_Data
        Return NewReceiptData(lineType, fontType, leftText, centerText, rightText, nameText, isUnderLine, 0, 0, 0)
    End Function

    Public Overloads Shared Function NewReceiptData(ByVal lineType As Integer, ByVal fontType As Integer, ByVal leftText As String, _
    ByVal centerText As String, ByVal rightText As String, ByVal isUnderLine As Boolean) As PrintReceiptOrder_Data
        Return NewReceiptData(lineType, fontType, leftText, centerText, rightText, "", False)
    End Function

    Public Overloads Shared Function NewReceiptData(ByVal lineType As Integer, ByVal fontType As Integer, ByVal leftText As String, _
    ByVal centerText As String, ByVal rightText As String) As PrintReceiptOrder_Data
        Return NewReceiptData(lineType, fontType, leftText, centerText, rightText, False)
    End Function

End Class

Public Class PrintDataListToPrinter_Data
    Public LeftText As String
    Public RightText As String
    Public CenterText As String
    Public PrintLineType As ReceiptLineType
    Public PrintFont As Font
    Public IsUnderLine As Boolean

    Public Enum ReceiptLineType
        Left
        Center
        Right
        Content2Column
        Content3Column
        AcrossLine
        DotLine
        BlankLine
        BarCode
    End Enum

    Public Shared Function NewPrintReceiptData(ByVal leftText As String, ByVal centerText As String, _
    ByVal rightText As String, ByVal printFont As Font, ByVal lineType As ReceiptLineType, _
    ByVal isUnderLine As Boolean) As PrintDataListToPrinter_Data
        Dim rData As New PrintDataListToPrinter_Data
        rData.LeftText = leftText
        rData.CenterText = centerText
        rData.RightText = rightText
        rData.PrintFont = printFont
        rData.PrintLineType = lineType
        rData.IsUnderLine = isUnderLine
        Return rData
    End Function

    Public Shared Function NewPrintReceiptDataFor2Column(ByVal leftText As String, ByVal rightText As String, _
    ByVal printFont As Font) As PrintDataListToPrinter_Data
        Return NewPrintReceiptData(leftText, "", rightText, printFont, ReceiptLineType.Content2Column, False)
    End Function

    Public Shared Function NewPrintReceiptDataFor3Column(ByVal leftText As String, ByVal centerText As String, _
    ByVal rightText As String, ByVal printFont As Font) As PrintDataListToPrinter_Data
        Return NewPrintReceiptData(leftText, centerText, rightText, printFont, _
                ReceiptLineType.Content3Column, False)
    End Function

    Public Shared Function NewPrintReceiptDataForDotLine(ByVal printFont As Font) As PrintDataListToPrinter_Data
        Return NewPrintReceiptData("", "", "", printFont, ReceiptLineType.DotLine, False)
    End Function

    Public Shared Function NewPrintReceiptDataForPrintAcrossPage(ByVal printText As String, ByVal printFont As Font) As PrintDataListToPrinter_Data
        Return NewPrintReceiptData(printText, "", "", printFont, ReceiptLineType.AcrossLine, False)
    End Function

    Public Shared Function NewPrintReceiptDataForPrintLeft(ByVal leftText As String, ByVal printFont As Font, _
    ByVal isUnderLine As Boolean) As PrintDataListToPrinter_Data
        Return NewPrintReceiptData(leftText, "", "", printFont, ReceiptLineType.Left, isUnderLine)
    End Function

    Public Shared Function NewPrintReceiptDataForPrintRight(ByVal rightText As String, ByVal printFont As Font, _
    ByVal isUnderLine As Boolean) As PrintDataListToPrinter_Data
        Return NewPrintReceiptData("", "", rightText, printFont, ReceiptLineType.Right, isUnderLine)
    End Function

    Public Shared Function NewPrintReceiptDataForPrintCenter(ByVal centerText As String, ByVal printFont As Font, _
    ByVal isUnderLine As Boolean) As PrintDataListToPrinter_Data
        Return NewPrintReceiptData("", centerText, "", printFont, ReceiptLineType.Center, isUnderLine)
    End Function

    Public Shared Function NewPrintReceiptDataForPrintBarCode(ByVal barCodeText As String, ByVal printFont As Font) As PrintDataListToPrinter_Data
        Return NewPrintReceiptData(barCodeText, "", "", printFont, ReceiptLineType.BarCode, False)
    End Function

    Public Shared Function NewPrintReceiptDataForBlankLine(ByVal printFont As Font) As PrintDataListToPrinter_Data
        Return NewPrintReceiptData("", "", "", printFont, ReceiptLineType.BlankLine, False)
    End Function

End Class

Public Class MagneticReaderProperty
    Public TrackDelimiter As String
    Public DataTrackNo As Integer
    Public ReadingType As MagneticReadingType
    Public StartPosition As Integer
    Public EndPosition As Integer
    Public PositionFromBack As Integer
    Public EndDataDelimiter As String

    Public Enum MagneticReadingType
        FromStartPositionToEndPosition
        FromStartPositionToPositionFromBack
        FromStartPositionToEndDataDelimiter
    End Enum

    Public Shared Function NewMagneticRederData(ByVal trackDelimiter As String, ByVal dataTrackNo As Integer, _
    ByVal readingID As Integer, ByVal startPosition As Integer, ByVal endPosition As Integer, ByVal positionFromBack As Integer, _
    ByVal endDataDelimiter As String) As MagneticReaderProperty
        Dim mReader As New MagneticReaderProperty
        mReader.TrackDelimiter = trackDelimiter
        mReader.DataTrackNo = dataTrackNo
        Select Case readingID
            Case 1
                mReader.ReadingType = MagneticReadingType.FromStartPositionToEndPosition
            Case 2
                mReader.ReadingType = MagneticReadingType.FromStartPositionToPositionFromBack
            Case 3
                mReader.ReadingType = MagneticReadingType.FromStartPositionToEndDataDelimiter
        End Select
        If startPosition <= 0 Then
            mReader.StartPosition = 1
        Else
            mReader.StartPosition = startPosition
        End If
        If endPosition < 0 Then
            mReader.EndPosition = 0
        Else
            mReader.EndPosition = endPosition
        End If
        If positionFromBack < 0 Then
            mReader.PositionFromBack = 0
        Else
            mReader.PositionFromBack = positionFromBack
        End If
        mReader.EndDataDelimiter = endDataDelimiter
        Return mReader
    End Function


End Class

Public Class Data_CustomerDetail
    Public iMemberID As Integer
    Public iMemberGroupID As Integer
    Public szMemberGroupName As String
    Public szMemberCode As String
    Public szCustomerFirstName As String
    Public szCustomerLastName As String
    Public szCustomerFullName As String
    Public szQueueName As String
    Public iCustomerMainPrice As Integer
End Class

Public Class Data_CustomerAddressDetail
    Public szCustomerAddress1 As String
    Public szCustomerAddress2 As String
    Public szCustomerCity As String
    Public szCustomerProvince As String
    Public iCustomerProvinceID As Integer
    Public szCustomerZipCode As String
    Public szCustomerTelephone As String
    Public szCustomerFax As String
    Public szCustomerMobile As String
    Public szCustomerEmail As String
    Public szCustomerNote As String
End Class

Public Class Data_StaffDetail
    Public szStaffCode As String
    Public iStaffID As Integer
    Public szStaffFirstName As String
    Public szStaffLastName As String
    Public szStaffFullName As String
    Public iStaffRoleID As Integer
    Public szStaffRoleName As String
End Class

Public Class Data_ProductPriceVAT
    Public fPriceNoVAT As Decimal
    Public fPriceExcludeVAT As Decimal
    Public fPriceIncludeVAT As Decimal
End Class

Public Class Data_ServiceChargePrice
    Public fServiceChargeWithoutVAT As Decimal
    Public fServiceChargeExcludeVAT As Decimal
    Public fServiceChargePrice As Decimal
End Class

Public Class ReceiptPrinterDetail_Data
    Public PrinterName As String
    Public PaperWidth As Single
    Public MarginLeft As Integer
    Public MarginTop As Integer
End Class

Public Class ProductPriceAndPromotion_Data
    Public RetailPrice As Decimal
    Public SalePrice As Decimal
    Public MinimumPrice As Decimal
    Public PromotionPriceID As Integer
    Public PromotionNPriceID As Integer
    Public DiscountAllow As Integer
    Public PromotionAmountType As Integer
End Class

Public Class ProductEnableExpireForAdd_Data
    Public EnableDateTime As DateTime
    Public ExpireDateTime As DateTime
    Public EnableDayString As String

    Public Overloads Shared Function NewProductEnableExpireTime(ByVal rProduct As DataRow) As ProductEnableExpireForAdd_Data
        Dim dayString As String
        If IsDBNull(rProduct("ProductEnableDateTime")) Then
            rProduct("ProductEnableDateTime") = Date.MinValue
        End If
        If IsDBNull(rProduct("ProductExpireDateTime")) Then
            rProduct("ProductExpireDateTime") = Date.MinValue
        End If
        Try
            If Not IsDBNull(rProduct("ProductEnableDayString")) Then
                dayString = rProduct("ProductEnableDayString")
            Else
                dayString = ""
            End If
        Catch ex As Exception
            dayString = ""
        End Try
        Return NewProductEnableExpireTime(rProduct("ProductEnableDateTime"), rProduct("ProductExpireDateTime"), dayString)
    End Function

    Public Overloads Shared Function NewProductEnableExpireTime(ByVal enableDateTime As DateTime, ByVal expireDateTime As DateTime, _
    ByVal enableDayString As String) As ProductEnableExpireForAdd_Data
        Dim pData As New ProductEnableExpireForAdd_Data
        pData.EnableDateTime = enableDateTime
        pData.ExpireDateTime = expireDateTime
        pData.EnableDayString = enableDayString
        Return pData
    End Function


End Class

Public Class Data_TransactionDetail
    Public TransactionID As Integer
    Public ComputerID As Integer
    Public SaleDate As Date
    Public TableID As Integer
    Public TransacionName As String
    Public QueueName As String
    Public OpenTime As DateTime
    Public BeginTime As DateTime
    Public EndTime As DateTime
    Public PrintWarningTime As DateTime
    Public NoCustomer As Integer
    Public NoCustomerWhenOpen As Integer
    Public CallForCheckBill As Integer
    Public NoPrintBillDetail As Integer
    Public BillDetailReferenceNo As Integer
    Public HasOrder As Boolean
    Public IsSplitTransaction As Boolean
    Public IsFromOtherTransaction As Integer
    Public IsPaymentComplete As Boolean
    Public IsOtherReceipt As Boolean
    Public SplitNo As Integer
    Public SaleMode As Integer
    Public TransactionStatus As Integer
    Public ReferenceNo As String

    Public SplitFromTransactionID As Integer
    Public SplitFromComputerID As Integer

    Public FromDepositTransactionID As Integer
    Public FromDepositComputerID As Integer

    Public TransactionSummary As Data_TransactionSummary
    Public OrderList As List(Of Data_OrderDetail)
    Public OtherIncomeList As List(Of Data_OtherIncome)
    Public TransactionDiscountDetail As TransactionDiscountDetail_Data
    Public CustomerDetail As Data_CustomerDetail

    Public Sub New()

        TransactionSummary = New Data_TransactionSummary
        OrderList = New List(Of Data_OrderDetail)
        OtherIncomeList = New List(Of Data_OtherIncome)
        TransactionDiscountDetail = New TransactionDiscountDetail_Data
        CustomerDetail = New Data_CustomerDetail
        SaleDate = Now
        IsPaymentComplete = False
        IsOtherReceipt = False
    End Sub
End Class

Public Class Data_TransactionSummary
    Public TransactionID As Integer
    Public ComputerID As Integer
    Public fSubTotalPrice As Decimal
    Public fUnSubmitPrice As Decimal
    Public bHasUnFinishOrder As Boolean
    Public OrderTransactionPrice As Data_ProductPriceVAT
    Public TransactionProductVAT As Data_ProductPriceVAT
    Public ServiceCharge As Data_ServiceChargePrice

    Public fDiscount_OtherPercent As Decimal
    Public fDiscount_OtherEachProduct As Decimal
    Public fDiscount_OtherAmount As Decimal
    Public fDiscount_OtherSummary As Decimal
    Public fDiscount_PricePromotion As Decimal
    Public fDiscount_PriceNPromotion As Decimal
    Public fDiscount_Member As Decimal
    Public fDiscount_Staff As Decimal
    Public fDiscount_Coupon As Decimal
    Public fDiscount_Voucher As Decimal
    Public fDiscount_Summary As Decimal

    Public fOtherIncomeSummary As Decimal
    Public fOtherIncomeSummaryVAT As Decimal

    Public SummaryByPromotionNameList As List(Of Data_DiscountSummaryByPromotionName)
    Public DisplaySummaryList As List(Of Data_DisplayColumnAndValue)

    Public fGrandTotalPrice As Decimal
    Public fTotalSalePrice As Decimal
    Public fTotalProductAmount As Decimal
    Public fTotalRetailPrice As Decimal

    Public fTransactionVAT As Decimal
    Public fTransactionVATAble As Decimal

    Public Sub New()
        OrderTransactionPrice = New Data_ProductPriceVAT
        TransactionProductVAT = New Data_ProductPriceVAT
        SummaryByPromotionNameList = New List(Of Data_DiscountSummaryByPromotionName)
        DisplaySummaryList = New List(Of Data_DisplayColumnAndValue)
    End Sub

    Public Sub CalculateSummaryDiscount()
        fDiscount_Summary = fDiscount_OtherPercent + fDiscount_OtherEachProduct + fDiscount_OtherAmount + _
                                        fDiscount_PriceNPromotion + fDiscount_PricePromotion + _
                                        fDiscount_Member + fDiscount_Staff + fDiscount_Coupon + fDiscount_Voucher

        fDiscount_OtherSummary = fDiscount_OtherAmount + fDiscount_OtherEachProduct + fDiscount_OtherPercent
    End Sub

End Class

Public Class Data_AddEditOtherIncome
    Public IncomeTypeID As Integer
    Public IncomePrice As Decimal

End Class

Public Class Data_OtherIncome
    Public IncomeID As Integer
    Public IncomeTypeID As Integer
    Public IncomeCode As String
    Public IncomeDisplayName As String
    Public IncomeName As String
    Public IncomePrice As Decimal
    Public IncomeVAT As Decimal
    Public IncomeNote As String
    Public VATType As Integer
    Public IncomePercent As Decimal
    Public IsManualIncome As Boolean
    Public ForPayID As Integer

    Public Shared Function NewOtherIncome(ByVal incomeID As Integer, ByVal incomeTypeID As Integer, ByVal incomeCode As String, _
    ByVal incomeDisplayName As String, ByVal incomeName As String, ByVal price As Decimal, ByVal VAT As Decimal, ByVal VATType As Integer, _
    ByVal incomeNote As String, ByVal incomePercent As Decimal, ByVal isManualIncome As Boolean, ByVal forPayID As Integer) As Data_OtherIncome
        Dim oData As New Data_OtherIncome
        oData.IncomeID = incomeID
        oData.IncomeTypeID = incomeTypeID
        oData.IncomeCode = incomeCode
        oData.IncomeDisplayName = incomeDisplayName
        oData.IncomeName = incomeName
        oData.IncomePrice = price
        oData.IncomeVAT = VAT
        oData.VATType = VATType
        oData.IncomeNote = incomeNote
        oData.IncomePercent = incomePercent
        oData.IsManualIncome = isManualIncome
        oData.ForPayID = forPayID
        Return oData
    End Function

    Public Shared Function GetNewIncomeID(ByVal otherIncomeList As List(Of Data_OtherIncome)) As Integer
        Dim oData As Data_OtherIncome
        Dim newID As Integer
        newID = 0
        For Each oData In otherIncomeList
            If newID < oData.IncomeID Then
                newID = oData.IncomeID
            End If
        Next
        Return newID + 1
    End Function

    Public Shared Function CopyOtherIncomeData(ByVal originalData As Data_OtherIncome) As Data_OtherIncome
        Return NewOtherIncome(originalData.IncomeID, originalData.IncomeTypeID, originalData.IncomeCode, originalData.IncomeDisplayName, _
                    originalData.IncomeName, originalData.IncomePrice, originalData.IncomeVAT, originalData.VATType, originalData.IncomeNote, _
                    originalData.IncomePercent, originalData.IsManualIncome, originalData.ForPayID)
    End Function

End Class


Public Class Data_DisplayColumnAndValue
    Public szDisplayName As String
    Public fPriceValue As Decimal
    Public bWarningValue As Boolean

    Public Shared Function NewDisplayColumnAndValue(ByVal name As String, ByVal value As Decimal, ByVal warningValue As Boolean) As Data_DisplayColumnAndValue
        Dim dData As New Data_DisplayColumnAndValue
        dData.szDisplayName = name
        dData.fPriceValue = value
        dData.bWarningValue = warningValue
        Return dData
    End Function

End Class

Public Class Data_DiscountSummaryByPromotionName
    Public PromotionTypeID As Integer
    Public PromotionName As String
    Public PromotionDiscountPrice As Decimal

    Public Shared Function NewSummaryByPromotionName(ByVal promoTypeID As Integer, ByVal promoName As String, _
    ByVal promoDiscountPrice As Decimal) As Data_DiscountSummaryByPromotionName
        Dim promoData As New Data_DiscountSummaryByPromotionName
        promoData.PromotionTypeID = promoTypeID
        promoData.PromotionName = promoName
        promoData.PromotionDiscountPrice = promoDiscountPrice
        Return promoData
    End Function
End Class

Public Class Data_OrderDetail
    Public iOrderID As Integer
    Public iSplitNo As Integer
    Public iProductID As Integer
    Public szProductCode As String
    Public szProductName As String
    Public fAmount As Decimal
    Public fTotalPrice As Decimal
    Public fPricePerUnit As Decimal
    Public fRetailPricePerUnit As Decimal
    Public szOrderComment As String
    Public iOrderStatus As Integer
    Public iVATType As Integer
    Public iPromotionAmountType As Integer
    Public iProductSetType As Integer
    Public bIsProductInSetWithPrice As Boolean
    Public bHasServiceCharge As Boolean
    Public iNoPrintBill As Integer
    Public iPricePromotionID As Integer
    Public iPriceNPromotionID As Integer
    Public bIsParentOrder As Boolean
    Public bIsComment As Boolean
    Public iOrderLinkID As Integer
    Public iSaleMode As Integer
    Public iReturnOrderType As Integer
    Public totalPriceRoundingType As RoundType

    Public iPrintOrder_Status As Integer
    Public dPrintOrder_InsertDate As DateTime

    Public Shared Sub AddOrderDetailDataIntoList(ByRef orderList As List(Of Data_OrderDetail), ByVal orderID As Integer, _
    ByVal splitNo As Integer, ByVal productID As Integer, ByVal productCode As String, _
    ByVal productName As String, ByVal amount As Decimal, ByVal totalPrice As Decimal, ByVal pricePerUnit As Decimal, _
    ByVal retailPricePerUnit As Decimal, ByVal orderComment As String, _
    ByVal orderStatus As Integer, ByVal VATType As Integer, ByVal promoAmountType As Integer, ByVal productSet As Integer, _
    ByVal isProductInSetWithPrice As Boolean, ByVal hasServiceCharge As Integer, _
    ByVal noPrintBill As Integer, ByVal pricePromoID As Integer, ByVal priceNPromoID As Integer, _
    ByVal isParentOrder As Integer, ByVal isComment As Integer, ByVal orderLinkID As Integer, ByVal saleMode As Integer, _
    ByVal jobOrderStatus As Integer, ByVal jobOrderInsertDate As DateTime, ByVal returnOrderType As Integer, _
    ByVal totalPriceRoundingType As RoundType, ByVal insertAtBeginList As Boolean)
        Dim dData As Data_OrderDetail
        dData = NewOrderDetailData(orderID, splitNo, productID, productCode, productName, amount, totalPrice, pricePerUnit, _
                        retailPricePerUnit, orderComment, orderStatus, VATType, promoAmountType, productSet, isProductInSetWithPrice, _
                        hasServiceCharge, noPrintBill, pricePromoID, priceNPromoID, isParentOrder, isComment, orderLinkID, _
                        saleMode, returnOrderType, totalPriceRoundingType, jobOrderStatus, jobOrderInsertDate)
        If insertAtBeginList = True Then
            orderList.Insert(0, dData)
        Else
            orderList.Add(dData)
        End If
    End Sub

    Public Shared Function CopyOrderDetail(ByVal copyOrder As Data_OrderDetail) As Data_OrderDetail
        Dim cData As Data_OrderDetail
        Dim isParent, isComment, hasServiceCharge As Integer
        If copyOrder.bIsParentOrder = True Then
            isParent = 1
        Else
            isParent = 0
        End If
        If copyOrder.bHasServiceCharge = True Then
            hasServiceCharge = 1
        Else
            hasServiceCharge = 0
        End If
        If copyOrder.bIsComment = True Then
            isComment = 1
        Else
            isComment = 0
        End If
        cData = NewOrderDetailData(copyOrder.iOrderID, copyOrder.iSplitNo, copyOrder.iProductID, copyOrder.szProductCode, copyOrder.szProductName, _
                    copyOrder.fAmount, copyOrder.fTotalPrice, copyOrder.fPricePerUnit, copyOrder.fRetailPricePerUnit, copyOrder.szOrderComment, _
                    copyOrder.iOrderStatus, copyOrder.iVATType, copyOrder.iPromotionAmountType, _
                    copyOrder.iProductSetType, copyOrder.bIsProductInSetWithPrice, hasServiceCharge, _
                    copyOrder.iNoPrintBill, copyOrder.iPricePromotionID, copyOrder.iPriceNPromotionID, isParent, isComment, _
                    copyOrder.iOrderLinkID, copyOrder.iSaleMode, copyOrder.iReturnOrderType, copyOrder.totalPriceRoundingType, _
                    copyOrder.iPrintOrder_Status, copyOrder.dPrintOrder_InsertDate)
        Return cData
    End Function

    Public Shared Function NewOrderDetailData(ByVal orderID As Integer, ByVal splitNo As Integer, ByVal productID As Integer, ByVal productCode As String, _
    ByVal productName As String, ByVal amount As Decimal, ByVal totalPrice As Decimal, ByVal pricePerUnit As Decimal, _
    ByVal retailPricePerUnit As Decimal, ByVal orderComment As String, _
    ByVal orderStatus As Integer, ByVal VATType As Integer, ByVal promoAmountType As Integer, ByVal productSet As Integer, _
    ByVal isProductInSetWithPrice As Boolean, ByVal hasServiceCharge As Integer, _
    ByVal noPrintBill As Integer, ByVal pricePromoID As Integer, ByVal priceNPromoID As Integer, _
    ByVal isParentOrder As Integer, ByVal isComment As Integer, ByVal orderLinkID As Integer, ByVal saleMode As Integer, _
    ByVal returnOrderType As Integer, ByVal totalPriceRoundingType As RoundType, ByVal jobOrderStatus As Integer, _
    ByVal jobOrderInsertDate As DateTime) As Data_OrderDetail
        Dim dData As New Data_OrderDetail
        Dim bolIsParent As Boolean
        Dim bolIsComment As Boolean
        If isParentOrder = 1 Then
            bolIsParent = True
        Else
            bolIsParent = False
        End If
        If isComment = 1 Then
            bolIsComment = True
        Else
            bolIsComment = False
        End If
        dData.iOrderID = orderID
        dData.iSplitNo = splitNo
        dData.iProductID = productID
        dData.szProductCode = productCode
        dData.szProductName = productName
        dData.fAmount = amount
        dData.fTotalPrice = totalPrice
        dData.fPricePerUnit = pricePerUnit
        dData.fRetailPricePerUnit = retailPricePerUnit
        dData.szOrderComment = orderComment
        dData.iOrderStatus = orderStatus
        dData.iVATType = VATType
        dData.iPromotionAmountType = promoAmountType
        dData.iProductSetType = productSet
        dData.bIsProductInSetWithPrice = isProductInSetWithPrice
        dData.bHasServiceCharge = hasServiceCharge
        dData.iPricePromotionID = pricePromoID
        dData.iPriceNPromotionID = priceNPromoID
        dData.iNoPrintBill = noPrintBill
        dData.bIsParentOrder = bolIsParent
        dData.bIsComment = bolIsComment
        dData.iOrderLinkID = orderLinkID
        dData.iSaleMode = saleMode
        dData.iReturnOrderType = returnOrderType
        dData.totalPriceRoundingType = totalPriceRoundingType
        dData.iPrintOrder_Status = jobOrderStatus
        dData.dPrintOrder_InsertDate = jobOrderInsertDate
        Return dData
    End Function


End Class

Public Class TransactionDiscountDetail_Data
    Public TransactionID As Integer
    Public ComputerID As Integer
    Public SplitNo As Integer
    Public TransactionOpenTime As DateTime

    Public MemberDiscountID As Integer
    Public MemberPriceGroupID As Integer
    Public MemberOverPrice As Decimal
    Public MemberDiscountAmountType As Integer
    Public MemberAllowOtherPromo As Integer
    Public MemberDiscountFromMinPriceToMax As Integer
    Public MemberCode As String
    Public MemberName As String
    Public MemberPrintReceiptCopy As Integer
    Public MemberPromotionProperty As String

    Public StaffDiscountID As Integer
    Public StaffPriceGroupID As Integer
    Public StaffOverPrice As Decimal
    Public StaffDiscountAmountType As Integer
    Public StaffAllowOtherPromo As Integer
    Public StaffDiscountFromMinPriceToMax As Integer
    Public StaffCode As String
    Public StaffName As String
    Public StaffPrintReceiptCopy As Integer
    Public StaffPromotionProperty As String

    Public VoucherDetailList As List(Of VoucherDetail_Data)
    Public VoucherTotalAmount As Decimal
    Public VoucherTotalUseAmount As Decimal

    Public CouponDetailList As List(Of CouponDetail_Data)

    Public OtherDiscountType As POSTypeClass.OtherDiscountType
    Public OtherAmountDiscount As Decimal
    Public OtherPercentDiscount As Decimal
    Public HasEachProductDiscount As Boolean

    Public CalculateDiscountFrom As POSTypeClass.CalculateDiscountProductBy
    Public IsCalculateServiceCharge As Boolean
    Public IsSkipCalculateExcludeVAT As Boolean

    Public Sub New()
        VoucherDetailList = New List(Of VoucherDetail_Data)
        CouponDetailList = New List(Of CouponDetail_Data)
        CalculateDiscountFrom = CalculateDiscountProductBy.Unknown
        IsCalculateServiceCharge = True
    End Sub

    Public Sub InsertNewVoucherDataIntoList(ByVal voucherID As Integer, ByVal voucherTypeID As Integer, _
    ByVal voucherComID As Integer, ByVal voucherHeader As String, ByVal voucherNo As String, ByVal isReuse As Integer, _
    ByVal isRequireMember As Integer, ByVal referenceNo As String, ByVal priceGroupID As Integer, _
    ByVal originalAmount As Decimal, ByVal useAmount As Decimal, ByVal isSale As Integer, ByVal overPrice As Decimal, _
    ByVal allowOtherPromo As Integer, ByVal allowOtherPromoSameLV As Integer, ByVal discountFromMinPriceToMax As Integer, _
    ByVal printReceiptCopy As Integer, ByVal promoProperty As String)
        Dim vData As New VoucherDetail_Data
        vData.VoucherID = voucherID
        vData.VoucherTypeID = voucherTypeID
        vData.VoucherComputerID = voucherComID
        vData.VoucherHeader = voucherHeader
        vData.VoucherNo = voucherNo
        vData.IsReuse = isReuse
        vData.IsRequireMember = isRequireMember
        vData.ReferenceNo = referenceNo
        vData.PriceGroupID = priceGroupID
        vData.OriginalAmount = originalAmount
        vData.UseAmount = useAmount
        If isSale = 1 Then
            vData.IsSale = True
        Else
            vData.IsSale = False
        End If
        vData.OverPrice = overPrice
        vData.AllowOtherPromo = allowOtherPromo
        vData.AllowOtherPromoSameLV = allowOtherPromoSameLV
        vData.VoucherDiscountFromMinPriceToMax = discountFromMinPriceToMax
        vData.VoucherPrintReceiptCopy = printReceiptCopy
        vData.VoucherPromotionProperty = promoProperty
        VoucherDetailList.Add(vData)
    End Sub

    Public Sub InsertNewCouponDataIntoList(ByVal couponID As Integer, ByVal couponTypeID As Integer, _
    ByVal couponComID As Integer, ByVal couponHeader As String, ByVal couponNo As String, ByVal isReuse As Integer, _
    ByVal isRequireMember As Integer, ByVal referenceNo As String, ByVal priceGroupID As Integer, _
    ByVal promoAmountType As Integer, ByVal overPrice As Decimal, ByVal useAmount As Decimal, _
    ByVal allowOtherPromo As Integer, ByVal allowOtherPromoSameLV As Integer, ByVal discountFromMinPriceToMax As Integer, _
    ByVal printReceiptCopy As Integer, ByVal promoProperty As String)
        Dim cData As New CouponDetail_Data
        cData.CouponID = couponID
        cData.CouponTypeID = couponTypeID
        cData.CouponComputerID = couponComID
        cData.CouponHeader = couponHeader
        cData.CouponNo = couponNo
        cData.IsReuse = isReuse
        cData.IsRequireMember = isRequireMember
        cData.ReferenceNo = referenceNo
        cData.PriceGroupID = priceGroupID
        cData.PromotionAmounType = promoAmountType
        cData.OverPrice = overPrice
        cData.UseAmount = useAmount
        cData.AllowOtherPromo = allowOtherPromo
        cData.AllowOtherPromoSameLV = allowOtherPromoSameLV
        cData.CouponDiscountFromMinPriceToMax = discountFromMinPriceToMax
        cData.CouponPrintReceiptCopy = printReceiptCopy
        cData.CouponPromotionProperty = promoProperty
        CouponDetailList.Add(cData)
    End Sub

    Public Function IsCouponAlreadyExist(ByVal couponID As Integer, ByVal couponTypeID As Integer) As Boolean
        Dim cData As CouponDetail_Data
        For Each cData In CouponDetailList
            If (cData.CouponID = couponID) And (cData.CouponTypeID = couponTypeID) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function IsVoucherAlreadyExist(ByVal voucherID As Integer, ByVal voucherTypeID As Integer) As Boolean
        Dim vData As VoucherDetail_Data
        For Each vData In VoucherDetailList
            If (vData.VoucherID = voucherID) And (vData.VoucherTypeID = voucherTypeID) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub ClearVoucherDiscountData()
        VoucherDetailList.Clear()
        VoucherTotalAmount = 0
        VoucherTotalUseAmount = 0
    End Sub

    Public Sub ClearMemberDiscountData()
        MemberDiscountID = 0
        MemberPriceGroupID = 0
        MemberOverPrice = 0
        MemberDiscountAmountType = 0
        MemberAllowOtherPromo = True
        MemberCode = ""
        MemberName = ""
        MemberPrintReceiptCopy = 0
        MemberPromotionProperty = ""
    End Sub

    Public Sub ClearStaffDiscountData()
        StaffDiscountID = 0
        StaffPriceGroupID = 0
        StaffOverPrice = 0
        StaffDiscountAmountType = 0
        StaffAllowOtherPromo = True
        StaffCode = ""
        StaffName = ""
        StaffPrintReceiptCopy = 0
        StaffPromotionProperty = ""
    End Sub

    Public Sub ClearOtherDiscountData()
        OtherDiscountType = OtherDiscountType.ByAmount
        OtherAmountDiscount = 0
        OtherPercentDiscount = 0
        HasEachProductDiscount = False
    End Sub

End Class

Public Class VoucherDetail_Data
    Public VoucherID As Integer
    Public VoucherTypeID As Integer
    Public VoucherComputerID As Integer
    Public VoucherHeader As String
    Public VoucherNo As String
    Public IsReuse As Integer
    Public IsRequireMember As Integer
    Public ReferenceNo As String
    Public PriceGroupID As Integer
    Public OriginalAmount As Decimal
    Public UseAmount As Decimal
    Public IsSale As Boolean
    Public OverPrice As Decimal
    Public AllowOtherPromo As Integer
    Public AllowOtherPromoSameLV As Integer
    Public VoucherDiscountFromMinPriceToMax As Integer
    Public VoucherPrintReceiptCopy As Integer
    Public VoucherPromotionProperty As String
End Class

Public Class CouponDetail_Data
    Public CouponID As Integer
    Public CouponTypeID As Integer
    Public CouponComputerID As Integer
    Public CouponHeader As String
    Public CouponNo As String
    Public IsReuse As Integer
    Public IsRequireMember As Integer
    Public ReferenceNo As String
    Public PriceGroupID As Integer
    Public PromotionAmounType As Integer
    Public OverPrice As Decimal
    Public UseAmount As Decimal
    Public AllowOtherPromo As Integer
    Public AllowOtherPromoSameLV As Integer
    Public CouponDiscountFromMinPriceToMax As Integer
    Public CouponPrintReceiptCopy As Integer
    Public CouponPromotionProperty As String
End Class

Public Class Data_SplitTransactionDetail
    Public OriginalTransactionID As Integer
    Public OriginalComputerID As Integer
    Public SplitTransactionID As Integer
    Public SplitComputerID As Integer
    Public SplitNo As Integer
    Public TransactionSaleDate As Date
    Public PaidStaffID As Integer
    Public NoOfCustomer As Integer
    Public SplitReceiptNo As String
    Public SplitOrderDetailList As List(Of Data_OrderDetail)
    Public SplitPaymentList As List(Of PaymentDetail_Data)
    Public SplitTransactionPriceSummary As Data_TransactionSummary
    Public SplitTransactionDiscountDetail As TransactionDiscountDetail_Data

    Public Shared Function NewSplitTransactionData(ByVal originalTransID As Integer, ByVal originalComID As Integer, _
    ByVal splitTransID As Integer, ByVal splitComID As Integer, ByVal splitNo As Integer, _
    ByVal SaleDate As Date, ByVal noCustomer As Integer) As Data_SplitTransactionDetail
        Dim splitData As New Data_SplitTransactionDetail
        splitData.OriginalTransactionID = originalTransID
        splitData.OriginalComputerID = originalComID
        splitData.SplitTransactionID = splitTransID
        splitData.SplitComputerID = splitComID
        splitData.SplitNo = splitNo
        splitData.TransactionSaleDate = SaleDate
        splitData.NoOfCustomer = noCustomer
        splitData.SplitOrderDetailList = New List(Of Data_OrderDetail)
        splitData.SplitPaymentList = New List(Of PaymentDetail_Data)
        splitData.SplitTransactionDiscountDetail = New TransactionDiscountDetail_Data
        splitData.SplitTransactionPriceSummary = New Data_TransactionSummary
        Return splitData
    End Function
End Class

Public Class Data_POSAuthorize
    Public AuthorizeStaffID As Integer
    Public AuthorizeStaffCode As String
    Public AuthorizeStaffRoleID As Integer
    Public AuthorizeStaffFirstName As String
    Public AuthorizeStaffLastName As String
    Public ReasonText As String
    Public ReasonID() As Integer
    Public AllReasonText As String

    Public Sub New()
        ReDim ReasonID(-1)
        ReasonText = ""
        AllReasonText = ""
    End Sub

End Class

Public Class Data_TableDetail
    Public iTableID As Integer
    Public iTableZoneID As Integer
    Public TableStatus As TableStatus
    Public szTableName As String
    Public iTableCapacity As Integer
    Public szCustomerName As String
    Public iNoOfCustomer As Integer
    Public iNoOfCustomerWhenOpen As Integer
    Public iMemberID As Integer
    Public bHasOrder As Boolean
    Public iNumberPrintBill As Integer
    Public dTableTime As DateTime
    Public bIsCombineTable As Boolean
    Public szCombineTableName As String
    Public iTransactionID As Integer
    Public iComputerID As Integer
    Public dBeginTime As DateTime
    Public dEndTime As DateTime
    Public dPrintWarningTime As DateTime
    Public iPrintBeginTime As Integer
    Public iCallForCheckBillStatus As Integer
    Public iCurrentAccessComputer As Integer
    Public bIsDummy As Boolean
    Public bIsSplitTransaction As Boolean
    Public iNoUnSubmitOrder As Integer

    Public Shared Function NewTableData(ByVal tableID As Integer, ByVal zoneID As Integer, ByVal tStatus As TableStatus, ByVal tableName As String, _
   ByVal tableCapacity As Integer, ByVal customerName As String, ByVal memberID As Integer, ByVal noCustomer As Integer, _
   ByVal noCustomerWhenOpen As Integer, ByVal hasOrder As Boolean, _
   ByVal noPrintBill As Integer, ByVal tableTime As DateTime, ByVal isCombineTable As Boolean, ByVal combineTableName As String, _
   ByVal transID As Integer, ByVal transComID As Integer, ByVal beginTime As DateTime, ByVal endTime As DateTime, _
   ByVal printWarningTime As DateTime, ByVal printBeginTime As Integer, ByVal callForCheckBill As Integer, ByVal currentAccess As Integer, _
   ByVal isDummy As Boolean, ByVal isSplitTrans As Boolean) As Data_TableDetail
        Return NewTableData(tableID, zoneID, tStatus, tableName, tableCapacity, customerName, memberID, noCustomer, noCustomerWhenOpen, _
                hasOrder, noPrintBill, tableTime, isCombineTable, combineTableName, transID, transComID, beginTime, endTime, _
                printWarningTime, printBeginTime, callForCheckBill, currentAccess, isDummy, isSplitTrans, 0)
    End Function

    Public Shared Function NewTableData(ByVal tableID As Integer, ByVal zoneID As Integer, ByVal tStatus As TableStatus, ByVal tableName As String, _
    ByVal tableCapacity As Integer, ByVal customerName As String, ByVal memberID As Integer, ByVal noCustomer As Integer, _
    ByVal noCustomerWhenOpen As Integer, ByVal hasOrder As Boolean, _
    ByVal noPrintBill As Integer, ByVal tableTime As DateTime, ByVal isCombineTable As Boolean, ByVal combineTableName As String, _
    ByVal transID As Integer, ByVal transComID As Integer, ByVal beginTime As DateTime, ByVal endTime As DateTime, _
    ByVal printWarningTime As DateTime, ByVal printBeginTime As Integer, ByVal callForCheckBill As Integer, ByVal currentAccess As Integer, _
    ByVal isDummy As Boolean, ByVal isSplitTrans As Boolean, ByVal noUnSubmitOrder As Integer) As Data_TableDetail
        Dim tData As New Data_TableDetail
        tData.iTableID = tableID
        tData.iTableZoneID = zoneID
        tData.TableStatus = tStatus
        tData.szTableName = tableName
        tData.iTableCapacity = tableCapacity
        tData.szCustomerName = customerName
        tData.iNoOfCustomer = noCustomer
        tData.iNoOfCustomerWhenOpen = noCustomerWhenOpen
        tData.bHasOrder = hasOrder
        tData.iNumberPrintBill = noPrintBill
        tData.dTableTime = tableTime
        tData.bIsCombineTable = isCombineTable
        tData.szCombineTableName = combineTableName
        tData.iTransactionID = transID
        tData.iComputerID = transComID
        tData.dBeginTime = beginTime
        tData.dEndTime = endTime
        tData.dPrintWarningTime = printWarningTime
        tData.iPrintBeginTime = printBeginTime
        tData.iCallForCheckBillStatus = callForCheckBill
        tData.iCurrentAccessComputer = currentAccess
        tData.bIsDummy = isDummy
        tData.bIsSplitTransaction = isSplitTrans
        tData.iNoUnSubmitOrder = noUnSubmitOrder
        Return tData
    End Function

End Class

Public Class Data_CommentProduct
    Public iCommentID As Integer
    Public iCommentDeptID As String
    Public szCommentCode As String
    Public szCommentName As String
    Public iProductSetType As Integer
    Public fCommentPrice As Decimal
    Public bRequireAddAmountForProduct As Boolean

    Public Shared Function NewCommentProduct(ByVal commentID As Integer, ByVal commentDeptID As Integer, ByVal commentCode As String, _
    ByVal commentName As String, ByVal productSetType As Integer, ByVal commentPrice As Decimal, ByVal requireAddAmountForProduct As Integer) As Data_CommentProduct
        Dim cData As New Data_CommentProduct
        cData.iCommentID = commentID
        cData.iCommentDeptID = commentDeptID
        cData.szCommentCode = commentCode
        cData.szCommentName = commentName
        cData.iProductSetType = productSetType
        cData.fCommentPrice = commentPrice
        If requireAddAmountForProduct = 1 Then
            cData.bRequireAddAmountForProduct = True
        Else
            cData.bRequireAddAmountForProduct = False
        End If
        Return cData
    End Function

End Class

Public Class Data_CommentForOrder
    Public iCommentID As Integer
    Public szCommentName As String
    Public fCommentAmount As Decimal
    Public fCommentPrice As Decimal
    Public iProductSetType As Integer

    Public Shared Function NewCommentForOrderProduct(ByVal commentID As Integer, ByVal commentName As String, ByVal commentAmount As Decimal, _
    ByVal commentPrice As Decimal, ByVal productSetType As Integer) As Data_CommentForOrder
        Dim cData As New Data_CommentForOrder
        cData.iCommentID = commentID
        cData.szCommentName = commentName
        cData.fCommentAmount = commentAmount
        cData.fCommentPrice = commentPrice
        cData.iProductSetType = productSetType
        Return cData
    End Function

End Class

Public Class Data_AddProductInTransaction
    Public iProductID As Integer
    Public fAddAmount As Decimal
End Class

Public Class Data_OrderAndAmount
    Public iOrderID As Integer
    Public fAddAmount As Decimal
End Class

Public Class Data_AddProductCodeToTransaction
    Public szProductCode As String
    Public fAddAmount As Decimal
    Public CommentList As List(Of Data_AddCommentCodeToTransaction)

    Public Sub New()
        szProductCode = ""
        fAddAmount = 0
        CommentList = New List(Of Data_AddCommentCodeToTransaction)
    End Sub
End Class

Public Class Data_AddCommentCodeToTransaction
    Public szCommentCode As String
    Public iAddAmount As Integer

    Public Sub New()
        szCommentCode = ""
        iAddAmount = 0
    End Sub

End Class


Public Class Data_KDSDetailForWebService
    Public TransactionID As Integer
    Public ComputerID As Integer
    Public SaleDate As Date
    Public ShopID As Integer
    Public QueueNo As Integer
    Public ReferenceNo As String
    Public NoCustomer As Integer
    Public MemberID As Integer
    Public MemberFirstName As String
    Public MemberLastName As String
    Public PreviousPrepaidAmount As Decimal
    Public CurrentPrepaidAmount As Decimal

End Class

Public Class Data_KDSTransactionDetail
    Public TransactionID As Integer
    Public ComputerID As Integer
    Public SaleDate As Date
    Public ShopID As Integer
    Public TableID As Integer
    Public KDSTransactionName As String
    Public StartDateTime As DateTime
    Public FinishDateTime As DateTime
    Public PickupDateTime As DateTime
    Public CancelDateTime As DateTime
    Public NoCustomer As Integer
    Public SaleMode As Integer
    Public KDSStatus As Integer

    Public KDSOrderList As List(Of Data_KDSOrderDetail)
    Public CustomerDetail As Data_CustomerDetail

    Public Sub New()
        KDSOrderList = New List(Of Data_KDSOrderDetail)
        CustomerDetail = New Data_CustomerDetail
        SaleDate = Now
    End Sub
End Class

Public Class Data_KDSOrderDetail
    Public iOrderID As Integer
    Public iProductID As Integer
    Public szProductCode As String
    Public szProductName As String
    Public fAmount As Decimal
    Public szOrderComment As String
    Public iOrderStatus As Integer
    Public bIsParentOrder As Boolean
    Public bIsComment As Boolean
    Public iOrderLinkID As Integer
    Public iSaleMode As Integer

    Public Shared Function NewKDSOrderDetail(ByVal orderID As Integer, ByVal productID As Integer, ByVal productCode As String, ByVal productName As String, _
    ByVal amount As Decimal, ByVal orderComment As String, ByVal orderStatus As Integer, ByVal isParentOrder As Boolean, ByVal isComment As Boolean, _
    ByVal orderLinkID As Integer, ByVal saleMode As Integer) As Data_KDSOrderDetail
        Dim kData As New Data_KDSOrderDetail
        kData.iOrderID = orderID
        kData.iProductID = productID
        kData.szProductCode = productCode
        kData.szProductName = productName
        kData.fAmount = amount
        kData.szOrderComment = orderComment
        kData.iOrderStatus = orderStatus
        kData.bIsParentOrder = isParentOrder
        kData.bIsComment = isComment
        kData.iOrderLinkID = orderLinkID
        kData.iSaleMode = saleMode
        Return kData
    End Function
End Class

Public Class Data_HoldTransaction
    Public iTransactionID As Integer
    Public iComputerID As Integer
    Public szCustomerName As String
    Public szQueueName As String
    Public iNoOfCustomer As Integer
    Public iTransactionStatus As Integer
    Public iMemberID As Integer
    Public dHoldDateTime As DateTime
    Public fTotalAmount As Decimal
    Public fTotalSalePrice As Decimal
    Public szFromComputerName As String

    Public Shared Function NewHoldTransactionData(ByVal transID As Integer, ByVal transComID As Integer, ByVal customerName As String, _
    ByVal queueName As String, ByVal noCustomer As Integer, ByVal memberID As Integer, ByVal transStatus As Integer, _
    ByVal holdDateTime As DateTime, ByVal totalAmount As Decimal, ByVal totalSalePrice As Decimal, ByVal fromComName As String) As Data_HoldTransaction
        Dim hData As New Data_HoldTransaction
        hData.iTransactionID = transID
        hData.iComputerID = transComID
        hData.iNoOfCustomer = noCustomer
        hData.szCustomerName = customerName
        hData.szQueueName = queueName
        hData.iMemberID = memberID
        hData.iTransactionStatus = transStatus
        hData.dHoldDateTime = holdDateTime
        hData.fTotalAmount = totalAmount
        hData.fTotalSalePrice = totalSalePrice
        hData.szFromComputerName = fromComName
        Return hData
    End Function
End Class

Public Class Data_DeliveryTransaction
    Public iTransactionID As Integer
    Public iComputerID As Integer
    Public szCustomerName As String
    Public szQueueName As String
    Public iMemberID As Integer
    Public szMemberCode As String
    Public szMemberFirstName As String
    Public szMemberLastName As String
    Public szAddress1 As String
    Public szAddress2 As String
    Public szCity As String
    Public iProvinceID As Integer
    Public szProvinceName As String
    Public szZipCode As String
    Public szDeliveryNote As String
    Public szTelephoneNumber As String
    Public szMobileNumber As String
    Public iDeliveryStatus As Integer
    Public dOrderTime As DateTime
    Public fTotalAmount As Decimal
    Public fTotalSalePrice As Decimal
    Public szFromComputerName As String

    Public Shared Function NewDeliveryTransactionData(ByVal transID As Integer, ByVal transComID As Integer, ByVal customerName As String, _
    ByVal queueName As String, ByVal memberID As Integer, ByVal memberCode As String, ByVal memberFirstName As String, ByVal memberLastName As String, _
    ByVal deliveryAddr1 As String, ByVal deliveryAddr2 As String, ByVal deliveryCity As String, _
    ByVal deliveryProvinceID As Integer, ByVal deliveryProvinceName As String, ByVal deliveryZipCode As String, ByVal deliveryNote As String, _
    ByVal deliveryTelephone As String, ByVal deliveryMobile As String, ByVal deliveryStatus As Integer, ByVal orderTime As DateTime, _
    ByVal totalAmount As Decimal, ByVal totalSalePrice As Decimal, ByVal fromComName As String) As Data_DeliveryTransaction
        Dim hData As New Data_DeliveryTransaction
        hData.iTransactionID = transID
        hData.iComputerID = transComID
        hData.szCustomerName = customerName
        hData.szQueueName = queueName
        hData.iMemberID = memberID
        hData.szMemberFirstName = memberFirstName
        hData.szMemberLastName = memberLastName
        hData.szMemberCode = memberCode
        hData.szAddress1 = deliveryAddr1
        hData.szAddress2 = deliveryAddr2
        hData.szCity = deliveryCity
        hData.iProvinceID = deliveryProvinceID
        hData.szProvinceName = deliveryProvinceName
        hData.szZipCode = deliveryZipCode
        hData.szDeliveryNote = deliveryNote
        hData.szTelephoneNumber = deliveryTelephone
        hData.szMobileNumber = deliveryMobile
        hData.iDeliveryStatus = deliveryStatus
        hData.dOrderTime = orderTime
        hData.fTotalAmount = totalAmount
        hData.fTotalSalePrice = totalSalePrice
        hData.szFromComputerName = fromComName
        Return hData
    End Function
End Class

Public Class Data_PaymentResult
    Public iTransactionID As Integer
    Public iComputerID As Integer
    Public szReceiptNo As String
    Public fTotalPayPrice As Decimal
    Public fCashChange As Decimal
    Public fPreviousRewardPoint As Decimal
    Public fTransactionRewardPoint As Decimal
    Public fTotalRewardPoint As Decimal

    Public iPaidStaffID As Integer
    Public bIsPrintReceipt As Boolean
    Public bIsPrintWirelessAccount As Boolean
    Public iNoPrintWirelessAccount As Integer
    Public arrayTransactionPayment As List(Of PaymentDetail_Data)

    Public dPaidTime As DateTime
    Public iReceptDocType As Integer


End Class

Public Class Data_SessionAccountInfo
    Public AccountID As Integer
    Public AccountShopID As Integer
    Public AccountDate As Date
    Public IsFromMultipleSession As Boolean

    Public SessionDetailList As List(Of Data_SessionInfo)

    Public AmountInAccount As Decimal
    Public AmountToBankAccount As Decimal
    Public AdjustAmount As Decimal
    Public AdjustCashChange As Decimal
    Public AccountFee As Decimal
    Public AccountReference As String
    Public AccountBankID As Integer
    Public AccountBankName As String
    Public AccountNumber As String
    Public AccountName As String

    Public DepositNote As String
    Public DepositStaffID As Integer
    Public DepositDateTime As DateTime

    Public BankInfo As List(Of Data_SessionBankInfo)
    Public SessionAccountStatus As Integer

    Public Sub New()
        SessionDetailList = New List(Of Data_SessionInfo)
        BankInfo = New List(Of Data_SessionBankInfo)
    End Sub

End Class

Public Class Data_SessionInfo
    Public SessionID As Integer
    Public SessionComputerID As Integer
    Public SessionDate As Date
    Public SessionShopID As Integer
    Public OpenSessionTime As DateTime
    Public OpenStaffID As Integer
    Public OpenSessionAmount As Decimal
    Public CloseSessionTime As DateTime
    Public CloseStaffID As Integer
    Public CloseSessionAmount As Decimal

End Class

Public Class Data_SessionBankInfo
    Public BankID As Integer
    Public BankName As String
    Public BankAccount As String
    Public BankAccountName As String
    Public BankDescription As String
    Public IsDefault As Boolean
End Class

Public Class Data_DepositTransactionDetail
    Public iTransactionID As Integer
    Public iComputerID As Integer
    Public dDepositDate As Date
    Public dPickUpDate As Date
    Public iDepositStatus As Integer
    Public szDepositName As String
    Public iMemberID As Integer
    Public szReceiptNo As String
    Public szDepositNote As String
    Public fTotalPrice As Decimal
    Public fDepositPrice As Decimal
    Public iPaidStaffID As Integer
    Public szPaidStaffCode As String
    Public szPaidStaffName As String
    Public dtDepositDateTime As DateTime
    Public dtPickupDateTime As DateTime

    'Public DepositOrderList As List(Of Data_KDSOrderDetail)
    Public CustomerAddressDetail As Data_CustomerAddressDetail

    Public Sub New()
        '   DepositOrderList = New List(Of Data_KDSOrderDetail)
        CustomerAddressDetail = New Data_CustomerAddressDetail
    End Sub

End Class

Public Class Data_DepositProductDetailByDate
    Public dViewDate As Date
    Public iProductID As Integer
    Public szProductCode As String
    Public szProductName As String
    Public iProductSetType As Integer
    Public fAmount As Decimal
    Public bViewByPickupDate As Boolean

End Class

Public Class Data_SearchProductInOrderResult
    Public iTransactionID As Integer
    Public iComputerID As Integer
    Public iOrderDetailID As Integer
    Public iSaleMode As Integer
    Public szTransactionName As String
    Public szQuueuName As String
    Public iProductID As Integer
    Public fAmount As Decimal
    Public fPrice As Decimal
    Public dSubmitOrderDateTime As DateTime
    Public iOrderStatusID As Integer
    Public iNoPrintBill As Integer
    Public iOrderStaffID As Integer
    Public szOrderStaffCode As String
    Public szOrderStaffFirstName As String
    Public szOrderStaffLastName As String
    Public iOrderComputerID As Integer
    Public szOrderComputerName As String
    Public szProductName As String
    Public iTableID As Integer
    Public szTableName As String

    Public Shared Function NewSearchProductResult(ByVal transID As Integer, ByVal transComID As Integer, ByVal orderID As Integer, ByVal transName As String, _
    ByVal saleMode As Integer, _
    ByVal queueName As String, ByVal productID As Integer, ByVal productName As String, ByVal amount As Decimal, ByVal price As Decimal, _
    ByVal submitOderDateTime As DateTime, ByVal orderStatusID As Integer, ByVal noPrintBill As Integer, ByVal orderStaffID As Integer, _
    ByVal orderStaffCode As String, ByVal orderStaffFirstName As String, ByVal orderStaffLastName As String, ByVal orderComID As Integer, _
    ByVal orderComName As String, ByVal tableID As Integer, ByVal tableName As String) As Data_SearchProductInOrderResult
        Dim sData As New Data_SearchProductInOrderResult
        sData.iTransactionID = transID
        sData.iComputerID = transComID
        sData.iOrderDetailID = orderID
        sData.iSaleMode = saleMode
        sData.szTransactionName = transName
        sData.szQuueuName = queueName
        sData.iProductID = productID
        sData.szProductName = productName
        sData.fAmount = amount
        sData.fPrice = price
        sData.dSubmitOrderDateTime = submitOderDateTime
        sData.iOrderStatusID = orderStatusID
        sData.iNoPrintBill = noPrintBill
        sData.iOrderStaffID = orderStaffID
        sData.szOrderStaffCode = orderStaffCode
        sData.szOrderStaffFirstName = orderStaffFirstName
        sData.szOrderStaffLastName = orderStaffLastName
        sData.iOrderComputerID = orderComID
        sData.szOrderComputerName = orderComName
        sData.iTableID = tableID
        sData.szTableName = tableName
        Return sData
    End Function

End Class

Public Class Data_SearchProductDetail
    Public ProductID As Integer
    Public ProductCode As String
    Public ProductBarCode As String
    Public ProductName As String
    Public ProductSet As Integer
    Public IsOutOfStock As Boolean
    Public EnableDateTime As DateTime
    Public ExpireDateTime As DateTime
    Public ProductPrice As Decimal

    Public Shared Function NewSearchProductResult(ByVal productID As Integer, ByVal productCode As String, ByVal productBarCode As String, _
    ByVal productName As String, ByVal productSet As Integer, ByVal isOutOfStock As Boolean, ByVal enableDateTime As DateTime, _
    ByVal expireDateTime As DateTime, ByVal productPrice As Decimal) As Data_SearchProductDetail
        Dim pData As New Data_SearchProductDetail
        pData.ProductID = productID
        pData.ProductCode = productCode
        pData.ProductBarCode = productBarCode
        pData.ProductName = productName
        pData.ProductSet = productSet
        pData.IsOutOfStock = isOutOfStock
        pData.EnableDateTime = enableDateTime
        pData.ExpireDateTime = expireDateTime
        pData.ProductPrice = productPrice
        Return pData
    End Function

End Class

<Serializable()> _
Public Class MaterialBarCodeReadingSetting
    Public CodeStartPosition As Integer
    Public CodeNoDigit As Integer
    Public AmountStartPosition As Integer
    Public AmountNoDigit As Integer
    Public NoDecimalForAmount As Integer
    Public PriceStartPosition As Integer
    Public PriceNoDigit As Integer
    Public NoDecimalForPrice As Integer
    Public IsTotalPriceInCode As Boolean

    Public Shared Function NewBarCodeReadingSetting(ByVal codeStartPos As Integer, ByVal codeNoDigit As Integer, ByVal amountStartPos As Integer, _
    ByVal amountNoDigit As Integer, ByVal noDecimalForAmount As Integer, ByVal priceStartPos As Integer, ByVal priceNoDigit As Integer, _
    ByVal noDecimalForPrice As Integer, ByVal isTotalPriceInCode As Integer) As MaterialBarCodeReadingSetting
        Dim mData As New MaterialBarCodeReadingSetting
        mData.CodeStartPosition = codeStartPos
        mData.CodeNoDigit = codeNoDigit
        mData.AmountStartPosition = amountStartPos
        mData.AmountNoDigit = amountNoDigit
        mData.NoDecimalForAmount = noDecimalForAmount
        mData.PriceStartPosition = priceStartPos
        mData.PriceNoDigit = priceNoDigit
        mData.NoDecimalForPrice = noDecimalForPrice
        If isTotalPriceInCode = 1 Then
            mData.IsTotalPriceInCode = True
        Else
            mData.IsTotalPriceInCode = False
        End If
        Return mData
    End Function


End Class

<Serializable()> _
Public Class StaffTextBarCodeReadingSetting
    Public ReadingType As StaffReadingType

    Public TextDelimiter As String

    Public CodeStartPosition As Integer
    Public CodeNoDigit As Integer
    Public PasswordStartPosition As Integer
    Public PasswordNoDigit As Integer

    Public UserNameUsePasswordChar As Boolean

    Public Sub New()
        UserNameUsePasswordChar = False
    End Sub

    Public Enum StaffReadingType
        FromStartPositionToEndPosition
        FromDelimiter
    End Enum

    Public Shared Function NewStaffTextReadingSetting(ByVal readType As Integer, ByVal codeStartPos As Integer, ByVal codeNoDigit As Integer, _
    ByVal passwordStartPos As Integer, ByVal passwordNoDigit As Integer, ByVal textDelimiter As String, _
    ByVal isUsePasswordChar As Integer) As StaffTextBarCodeReadingSetting
        Dim sData As New StaffTextBarCodeReadingSetting
        Select Case readType
            Case 2
                sData.ReadingType = StaffReadingType.FromDelimiter
            Case Else
                sData.ReadingType = StaffReadingType.FromStartPositionToEndPosition
        End Select

        sData.CodeStartPosition = codeStartPos
        sData.CodeNoDigit = codeNoDigit
        sData.PasswordStartPosition = passwordStartPos
        sData.PasswordNoDigit = passwordNoDigit

        sData.TextDelimiter = textDelimiter
        If isUsePasswordChar = 1 Then
            sData.UserNameUsePasswordChar = True
        Else
            sData.UserNameUsePasswordChar = False
        End If
        Return sData
    End Function


End Class

Public Class Data_PromotionInfo
    Public iPromotionID As Integer
    Public szPromotionName As String
    Public szDescription As String
    Public PromotionType As DiscountType

    Public Shared Function NewPromotionInfo(ByVal promoID As Integer, ByVal promoName As String, ByVal description As String, _
    ByVal promoType As DiscountType) As Data_PromotionInfo
        Dim pData As New Data_PromotionInfo
        pData.iPromotionID = promoID
        pData.szPromotionName = promoName
        pData.szDescription = description
        pData.PromotionType = promoType
        Return pData
    End Function


End Class

Public Class PayTypeFormat_Data

    Public DisplayInPaymentNoteReport As Boolean
    Public DisplayPaymentNoteInSession As Boolean

    Public Sub New()
        DisplayInPaymentNoteReport = False
        DisplayPaymentNoteInSession = False
    End Sub

    Public Const PROPERTY_DISPLAYINPAYMENTNOTEREPORT As Integer = 0
    Public Const PROPERTY_DISPLAYPAYMENTNOTE_INSESSION As Integer = 1

    Public Shared Function SetPaymentFormatProperty(ByVal paymentFormatText As String) As PayTypeFormat_Data
        Dim pdata As PayTypeFormat_Data
        pdata = New PayTypeFormat_Data
        Dim strProperty() As String
        If paymentFormatText = "" Then
            Return pdata
        End If
        If InStr(paymentFormatText, "|") > 0 Then
            strProperty = Split(paymentFormatText, "|")
        Else
            strProperty = Split(paymentFormatText, ",")
        End If

        If strProperty.Length >= 1 Then
            If IsNumeric(strProperty(PROPERTY_DISPLAYINPAYMENTNOTEREPORT)) Then
                If CInt(strProperty(PROPERTY_DISPLAYINPAYMENTNOTEREPORT)) = 1 Then
                    pdata.DisplayInPaymentNoteReport = True
                End If
            End If
        End If
        If strProperty.Length >= 2 Then
            If IsNumeric(strProperty(PROPERTY_DISPLAYPAYMENTNOTE_INSESSION)) Then
                If CInt(strProperty(PROPERTY_DISPLAYPAYMENTNOTE_INSESSION)) = 1 Then
                    pdata.DisplayPaymentNoteInSession = True
                End If
            End If
        End If
        Return pdata
    End Function

    Public Shared Function IsSetThisPropertyInPaymentProperty(ByVal paymentFormatText As String, ByVal propertyID As Integer) As Boolean
        Dim strProperty() As String
        Dim propertyLenght As Integer
        If paymentFormatText = "" Then
            Return False
        End If
        If InStr(paymentFormatText, "|") > 0 Then
            strProperty = Split(paymentFormatText, "|")
        Else
            strProperty = Split(paymentFormatText, ",")
        End If
        propertyLenght = propertyID + 1
        If strProperty.Length >= propertyLenght Then
            If IsNumeric(strProperty(propertyID)) Then
                If CInt(strProperty(propertyID)) = 1 Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function
End Class


Public Class DepositCashSetting
    Public HasDepositCashFeature As Boolean
    Public DepositAmount As Decimal
    Public LockAfterSaleTransaction As Integer
    Public ClearDepositWhenCloseSession As Integer

    Public CurrentDepositAmount As Decimal
    Public CurrentCashSaleAmount As Decimal

    Public Sub New()
        HasDepositCashFeature = False
        DepositAmount = 0
        LockAfterSaleTransaction = 0
        ClearDepositWhenCloseSession = 0

        CurrentCashSaleAmount = 0
        CurrentDepositAmount = 0
    End Sub

    Public Const CLEARDEPOSITWHENCLOSESESSION_NONE As Integer = 0
    Public Const CLEARDEPOSITWHENCLOSESESSION_LOCKAMOUNT As Integer = 1
    Public Const CLEARDEPOSITWHENCLOSESESSION_CANCHANGEAMOUNT As Integer = 2

    Public Const PROPERTY_DEPOSITAMOUNT As Integer = 0
    Public Const PROPERTY_LOCKAFTERSALETRANSACTION As Integer = 1
    Public Const PROPERTY_CLEARWHENCLOSESESSION As Integer = 2

    Public Shared Function SetDepositCashSetting(ByVal depositSettingText As String) As DepositCashSetting
        Dim dData As DepositCashSetting
        dData = New DepositCashSetting
        Dim strProperty() As String
        If depositSettingText = "" Then
            Return dData
        End If
        If InStr(depositSettingText, "|") > 0 Then
            strProperty = Split(depositSettingText, "|")
        Else
            strProperty = Split(depositSettingText, ",")
        End If

        If strProperty.Length >= 1 Then
            If IsNumeric(strProperty(PROPERTY_DEPOSITAMOUNT)) Then
                dData.DepositAmount = CDec(strProperty(PROPERTY_DEPOSITAMOUNT))
            End If
        End If
        If strProperty.Length >= 2 Then
            If IsNumeric(strProperty(PROPERTY_LOCKAFTERSALETRANSACTION)) Then
                dData.LockAfterSaleTransaction = CInt(strProperty(PROPERTY_LOCKAFTERSALETRANSACTION))
            End If
        End If
        If strProperty.Length >= 3 Then
            If IsNumeric(strProperty(PROPERTY_CLEARWHENCLOSESESSION)) Then
                dData.ClearDepositWhenCloseSession = CInt(strProperty(PROPERTY_CLEARWHENCLOSESESSION))
            End If
        End If
        If dData.DepositAmount <= 0 Then
            dData.HasDepositCashFeature = False
        Else
            dData.HasDepositCashFeature = True
        End If
        Return dData
    End Function
End Class

Public Class Data_DisplayOrderSummary
    Public szProductName As String
    Public fAmount As Decimal
    Public fPricePerUnit As Decimal
    Public fTotalPrice As Decimal
    Public dOrderTime As DateTime
    Public iorderStatus As Integer
    Public iProductSetType As Integer

    Public Shared Function NewDisplayOrderSummary(ByVal productName As String, ByVal amount As Decimal, ByVal pricePerUnit As Decimal, _
    ByVal totalPrice As Decimal, ByVal orderTime As DateTime, ByVal orderStatus As Integer, ByVal productSetType As Integer) As Data_DisplayOrderSummary
        Dim pData As New Data_DisplayOrderSummary
        pData.szProductName = productName
        pData.fAmount = amount
        pData.fPricePerUnit = pricePerUnit
        pData.fTotalPrice = totalPrice
        pData.dOrderTime = orderTime
        pData.iorderStatus = orderStatus
        pData.iProductSetType = productSetType
        Return pData
    End Function






End Class






