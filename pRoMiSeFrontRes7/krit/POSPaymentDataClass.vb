
Public Class PrintPaymentDetail_Data
    Public PayTypeID As Integer
    Public Amount As Decimal
    Public PaidByName As String
    Public CreditCardno As String
    Public CreditCardType As String
    Public SmartCardID As Integer
    Public PaymentVAT As Decimal

    Public Overloads Shared Sub InsertPrintPaymentDataIntoList(ByVal arrayPrintPayment As List(Of PrintPaymentDetail_Data), _
    ByVal payTypeID As Integer, ByVal amount As Decimal, ByVal paidByName As String, _
    ByVal creditCardNo As String, ByVal creditCardType As String, ByVal smartCardID As Integer, ByVal paymentVAT As Decimal)
        Dim printPayment As New PrintPaymentDetail_Data
        printPayment.PayTypeID = payTypeID
        printPayment.Amount = amount
        printPayment.PaidByName = paidByName
        printPayment.CreditCardno = creditCardNo
        printPayment.CreditCardType = creditCardType
        printPayment.SmartCardID = smartCardID
        printPayment.PaymentVAT = paymentVAT
        arrayPrintPayment.Add(printPayment)
    End Sub

    Public Overloads Shared Sub InsertPrintPaymentDataIntoList(ByVal arrayPrintPayment As List(Of PrintPaymentDetail_Data), _
   ByVal payTypeID As Integer, ByVal amount As Decimal, ByVal paidByName As String, _
     ByVal creditCardNo As String, ByVal smartCardID As Integer, ByVal paymentVAT As Decimal)
        InsertPrintPaymentDataIntoList(arrayPrintPayment, payTypeID, amount, paidByName, creditCardNo, _
             "", smartCardID, paymentVAT)
    End Sub

End Class

Public Class PaymentDetail_Data
    Public PayID As Integer
    Public PayTypeID As Integer
    Public PayTypecode As String
    Public PayTypeDisplayName As String
    Public IsVAT As Boolean
    Public IsOtherReceipt As Boolean
    Public IsPrintOtherReceipt As Integer
    Public IsPrepaid As Boolean
    Public IsOpenDrawer As Boolean
    Public ReceiptDocTypeID As Integer
    Public SaleMaterialDocTypeID As Integer
    Public PayTypeFunction As Integer
    Public PayPrice As Decimal
    Public CreditCardNumber As String
    Public CreditCardTypeID As Integer
    Public CreditCardType As String
    Public CreditCardBankID As Integer
    Public CreditCardExpireMonth As Integer
    Public CreditCardExpireYear As Integer
    Public CreditCardApprovalCode As String
    Public IsFromEDC As Integer
    Public ConvertPayTypeTo As Integer
    Public OriginalPayTypeID As Integer
    Public IsDisplayNameByOriginalPayType As Integer
    Public ChequeNumber As String
    Public ChequeDate As Date
    Public PaidBy As String
    Public PaidReasonID As Integer
    Public CardID As Integer
    Public CardProductLevelID As Integer
    Public CardCurrentAmountMoney As Decimal
    Public PrepaidDiscountPercent As Decimal
    Public RedeemPoint As Decimal
    Public AfterRedeemPoint As Decimal
    Public RedeemMemberID As Integer
    Public RedeemID As Integer
    Public RedeemPointPerUnit As Decimal
    Public RedeemPointPerPaymentAmount As Decimal
    Public RedeemTransactionID As Integer
    Public RedeemComputerID As Integer
    Public CashChange As Decimal
    Public PaymentVAT As Decimal
    Public CanEditDeleteInMultiplePayment As Integer
    Public DisableCancelInMultiplePayment As Integer
    Public PayTypePrintReceiptCopy As Integer

    Public Const CHECKSAMEPAYDETAIL_GROUP_SAMEDETAIL As Integer = 0
    Public Const CHECKSAMEPAYDETAIL_SPLIT_SAMEDETAIL As Integer = 1
    Public Const CHECKSAMEPAYDETAIL_NOTALLOW_SAMEDETAIL As Integer = 2

    Public Sub New()
        ChequeDate = Now
    End Sub

    Public Shared Sub InsertPaymentDataIntoList(ByVal arrayPaymentData As List(Of PaymentDetail_Data), _
    ByVal payID As Integer, ByVal payTypeID As Integer, ByVal payTypeCode As String, ByVal payTypeDisplayName As String, _
    ByVal isVAT As Boolean, ByVal isOtherReceipt As Boolean, ByVal isPrintOtherReciept As Integer, _
    ByVal isPrepaid As Boolean, ByVal isOpenDrawer As Boolean, ByVal receiptDocTypeID As Integer, _
    ByVal saleMaterialDocTypeID As Integer, ByVal payTypeFunction As Integer, ByVal payPrice As Decimal, _
    ByVal creditCardNumber As String, ByVal creditCardTypeID As Integer, ByVal creditCardType As String, _
    ByVal creditCardBankID As Integer, ByVal creditCardExpireMonth As Integer, ByVal creditCardExpireYear As Integer, _
    ByVal creditCardApprovalCode As String, ByVal chequeNumber As String, _
    ByVal isFromEDC As Integer, ByVal convertToPayType As Integer, ByVal isDisplayNameByOriginalPayType As Integer, _
    ByVal chequeDate As Date, ByVal paidBy As String, ByVal paidReasonID As Integer, _
    ByVal cardID As Integer, ByVal cardProductLevelID As Integer, _
    ByVal cardCurrentAmountMoney As Decimal, ByVal redeemID As Integer, ByVal redeemPoint As Decimal, ByVal afterRedeemPoint As Decimal, _
    ByVal redeemMemberID As Integer, ByVal redeemPointPerUnit As Decimal, ByVal redeemPointPerPayAmount As Decimal, _
    ByVal redeemTransID As Integer, ByVal redeemTransComID As Integer, _
    ByVal prepaidDiscountPercent As Decimal, ByVal cashChange As Decimal, ByVal paymentVAT As Decimal, ByVal printReceiptCopy As Integer)
        Dim paymentData As New PaymentDetail_Data
        paymentData.PayID = payID
        paymentData.PayTypeID = payTypeID
        paymentData.PayTypecode = payTypeCode
        paymentData.PayTypeDisplayName = payTypeDisplayName
        paymentData.IsVAT = isVAT
        paymentData.IsOtherReceipt = isOtherReceipt
        paymentData.IsPrintOtherReceipt = isPrintOtherReciept
        paymentData.IsPrepaid = isPrepaid
        paymentData.IsOpenDrawer = isOpenDrawer
        paymentData.ReceiptDocTypeID = receiptDocTypeID
        paymentData.SaleMaterialDocTypeID = saleMaterialDocTypeID
        paymentData.PayTypeFunction = payTypeFunction
        paymentData.PayPrice = payPrice
        paymentData.CreditCardNumber = creditCardNumber
        paymentData.CreditCardTypeID = creditCardTypeID
        paymentData.CreditCardType = creditCardType
        paymentData.CreditCardBankID = creditCardBankID
        paymentData.CreditCardExpireMonth = creditCardExpireMonth
        paymentData.CreditCardExpireYear = creditCardExpireYear
        paymentData.CreditCardApprovalCode = creditCardApprovalCode
        paymentData.IsFromEDC = isFromEDC
        paymentData.ConvertPayTypeTo = convertToPayType
        If convertToPayType <> 0 Then
            paymentData.OriginalPayTypeID = payTypeID
            paymentData.PayTypeID = convertToPayType
        Else
            paymentData.OriginalPayTypeID = 0
        End If
        paymentData.IsDisplayNameByOriginalPayType = isDisplayNameByOriginalPayType
        paymentData.ChequeNumber = chequeNumber
        paymentData.ChequeDate = chequeDate
        paymentData.PaidBy = paidBy
        paymentData.PaidReasonID = paidReasonID
        paymentData.CardID = cardID
        paymentData.CardProductLevelID = cardProductLevelID
        paymentData.CardCurrentAmountMoney = cardCurrentAmountMoney
        paymentData.PrepaidDiscountPercent = prepaidDiscountPercent
        paymentData.RedeemID = redeemID
        paymentData.RedeemMemberID = redeemMemberID
        paymentData.RedeemPointPerUnit = redeemPointPerUnit
        paymentData.RedeemPointPerPaymentAmount = redeemPointPerPayAmount
        paymentData.RedeemMemberID = redeemPoint
        paymentData.AfterRedeemPoint = afterRedeemPoint
        paymentData.RedeemTransactionID = redeemTransComID
        paymentData.RedeemComputerID = redeemTransComID
        paymentData.CashChange = cashChange
        paymentData.PaymentVAT = paymentVAT
        paymentData.PayTypePrintReceiptCopy = printReceiptCopy
        arrayPaymentData.Add(paymentData)
    End Sub

    Private Shared Function NewPayTypeIDInPayDetailList(ByVal arrayPayDetail As List(Of PaymentDetail_Data)) As Integer
        Dim maxID As Integer
        Dim paymentData As PaymentDetail_Data
        If arrayPayDetail.Count = 0 Then
            maxID = 1
        Else
            For Each paymentData In arrayPayDetail
                If paymentData.PayID > maxID Then
                    maxID = paymentData.PayID
                End If
            Next
            maxID += 1
        End If
        Return maxID
    End Function

    Private Shared Sub CalculatePaymentVATAndVATable(ByVal paymentData As PaymentDetail_Data, ByVal shopVAT As Decimal)
        Dim isCalVAT As Boolean
        Select Case paymentData.PayTypeID
            Case POSType.PayByCash, POSType.PayByCreditCard, POSType.PayByCheque
                isCalVAT = True
            Case POSType.PayBySmartcard, POSType.PayByPrepaidBarcode
                isCalVAT = False
            Case Else
                isCalVAT = paymentData.IsVAT
        End Select
        If isCalVAT = False Then
            paymentData.PaymentVAT = 0
        Else
            paymentData.PaymentVAT = (paymentData.PayPrice * shopVAT) / (100 + shopVAT)
        End If
    End Sub

    Private Shared Sub InsertPayTypeDetailIntoPaymentData(ByRef paymentData As PaymentDetail_Data, _
    ByVal dtPayType As DataTable, ByVal payTypeIndex As Integer)
        paymentData.PayTypeID = dtPayType.Rows(payTypeIndex)("PayTypeID")
        If Not IsDBNull(dtPayType.Rows(payTypeIndex)("PayTypeCode")) Then
            paymentData.PayTypecode = dtPayType.Rows(payTypeIndex)("PayTypeCode")
        Else
            paymentData.PayTypecode = ""
        End If
        If Not IsDBNull(dtPayType.Rows(payTypeIndex)("DisplayName")) Then
            paymentData.PayTypeDisplayName = dtPayType.Rows(payTypeIndex)("DisplayName")
        Else
            paymentData.PayTypeDisplayName = ""
        End If
        If dtPayType.Rows(payTypeIndex)("IsVAT") = 1 Then
            paymentData.IsVAT = True
        Else
            paymentData.IsVAT = False
        End If
        If dtPayType.Rows(payTypeIndex)("IsPrepaid") = 1 Then
            paymentData.IsPrepaid = True
        Else
            paymentData.IsPrepaid = False
        End If
        'IsOpenDrawer
        Try
            If dtPayType.Rows(payTypeIndex)("IsOpenDrawer") = 1 Then
                paymentData.IsOpenDrawer = True
            Else
                paymentData.IsOpenDrawer = False
            End If

        Catch ex As Exception
            paymentData.IsOpenDrawer = True
        End Try
        'IsFromEDC
        Try
            paymentData.IsFromEDC = dtPayType.Rows(payTypeIndex)("EDCType")
        Catch ex As Exception
            paymentData.IsFromEDC = 0
        End Try
        'ConvertToPayType
        paymentData.ConvertPayTypeTo = dtPayType.Rows(payTypeIndex)("ConvertPayTypeTo")
        If paymentData.ConvertPayTypeTo <> 0 Then
            paymentData.OriginalPayTypeID = paymentData.PayTypeID
            paymentData.PayTypeID = paymentData.ConvertPayTypeTo
        Else
            paymentData.OriginalPayTypeID = 0
        End If
        paymentData.IsDisplayNameByOriginalPayType = dtPayType.Rows(payTypeIndex)("IsDisplayNameByOriginalPayType")
        paymentData.PrepaidDiscountPercent = dtPayType.Rows(payTypeIndex)("PrepaidDiscountPercent")
        If dtPayType.Rows(payTypeIndex)("IsOtherReceipt") = 1 Then
            paymentData.IsOtherReceipt = True
        Else
            paymentData.IsOtherReceipt = False
        End If
        Try
            paymentData.IsPrintOtherReceipt = dtPayType.Rows(payTypeIndex)("IsPrintOtherReceipt")
        Catch ex As Exception
            paymentData.IsPrintOtherReceipt = 0
        End Try
        paymentData.PayTypeFunction = dtPayType.Rows(payTypeIndex)("PayTypeFunction")
        'Other Receipt
        If dtPayType.Rows(payTypeIndex)("IsOtherReceipt") = 1 Then
            paymentData.ReceiptDocTypeID = dtPayType.Rows(payTypeIndex)("SaleDocumentTypeID")
            paymentData.SaleMaterialDocTypeID = dtPayType.Rows(payTypeIndex)("SaleDocumentTypeID")
        Else
            paymentData.ReceiptDocTypeID = POSType.RECEIPT_NORMAL
            paymentData.SaleMaterialDocTypeID = 20
        End If
        'Can Edit Delete for MultiplePayment
        If dtPayType.Rows(payTypeIndex)("CanEditDeletePaymentInMultiple") = 1 Then
            paymentData.CanEditDeleteInMultiplePayment = dtPayType.Rows(payTypeIndex)("CanEditDeletePaymentInMultiple")
        Else
            paymentData.CanEditDeleteInMultiplePayment = 0
        End If
        'Disable Cancel Button In MultiplePayment if PayByThis PayType
        Try
            Select Case paymentData.PayTypeID
                Case POSType.PayBySmartcard
                    paymentData.DisableCancelInMultiplePayment = 1
                Case Else
                    If dtPayType.Rows(payTypeIndex)("CanEditDeletePaymentInMultiple") = 2 Then
                        paymentData.DisableCancelInMultiplePayment = 1
                    Else
                        paymentData.DisableCancelInMultiplePayment = 0
                    End If
            End Select
        Catch ex As Exception
            paymentData.DisableCancelInMultiplePayment = 0
        End Try
        Try
            paymentData.PayTypePrintReceiptCopy = dtPayType.Rows(payTypeIndex)("NoPrintReceiptCopy")
        Catch ex As Exception
            paymentData.PayTypePrintReceiptCopy = 0
        End Try
    End Sub

    Public Shared Function InsertOrUpdatePayByCashDetailInList(ByVal arrayPayDetail As List(Of PaymentDetail_Data), _
    ByVal dtPayType As DataTable, ByVal payTypeIndex As Integer, ByVal payPrice As Decimal, _
    ByVal changePrice As Decimal, ByVal shopVAT As Decimal) As Integer
        Dim i As Integer
        Dim bolSkipChecking As Boolean
        Dim paymentData As PaymentDetail_Data
        Try
            If dtPayType.Rows(payTypeIndex)("NotCheckSamePaymentDetail") = 1 Then
                bolSkipChecking = True
            Else
                bolSkipChecking = False
            End If
        Catch ex As Exception
            bolSkipChecking = False
        End Try
        If bolSkipChecking = False Then
            For i = 0 To arrayPayDetail.Count - 1
                paymentData = arrayPayDetail(i)
                If (paymentData.PayTypeID = POSType.PayByCash) Then
                    paymentData.PayPrice += payPrice
                    paymentData.CashChange = changePrice
                    CalculatePaymentVATAndVATable(paymentData, shopVAT)
                    Return i
                End If
            Next i
        End If
        'Add New Payment Into ArrayList
        paymentData = New PaymentDetail_Data
        paymentData.PayID = NewPayTypeIDInPayDetailList(arrayPayDetail)
        paymentData.PayPrice = payPrice
        paymentData.CashChange = changePrice
        arrayPayDetail.Add(paymentData)
        InsertPayTypeDetailIntoPaymentData(paymentData, dtPayType, payTypeIndex)
        CalculatePaymentVATAndVATable(paymentData, shopVAT)
        Return -1
    End Function

    Public Overloads Shared Function InsertOrUpdatePayByCreditCardDetailInList(ByVal arrayPayDetail As List(Of PaymentDetail_Data), _
    ByVal dtPayType As DataTable, ByVal payTypeIndex As Integer, ByVal payPrice As Decimal, ByVal creditcardNumber As String, _
    ByVal creditCardTypeID As Integer, ByVal bankID As Integer, ByVal creditcardExpireMonth As Integer, _
    ByVal creditcardExpireYear As Integer, ByVal creditcardApprovalCode As String, _
    ByVal shopVAT As Decimal) As Integer
        Return InsertOrUpdatePayByCreditCardDetailInList(arrayPayDetail, dtPayType, payTypeIndex, _
                payPrice, creditcardNumber, creditCardTypeID, "", bankID, creditcardExpireMonth, _
                creditcardExpireYear, creditcardApprovalCode, shopVAT)
    End Function

    Public Overloads Shared Function InsertOrUpdatePayByCreditCardDetailInList(ByVal arrayPayDetail As List(Of PaymentDetail_Data), _
    ByVal dtPayType As DataTable, ByVal payTypeIndex As Integer, ByVal payPrice As Decimal, ByVal creditcardNumber As String, _
    ByVal creditCardTypeID As Integer, ByVal creditCardType As String, ByVal bankID As Integer, _
    ByVal creditcardExpireMonth As Integer, ByVal creditcardExpireYear As Integer, _
    ByVal creditcardApprovalCode As String, ByVal shopVAT As Decimal) As Integer
        Dim i As Integer
        Dim paymentData As PaymentDetail_Data
        Dim bolSkipChecking As Boolean
        Try
            If dtPayType.Rows(payTypeIndex)("NotCheckSamePaymentDetail") = 1 Then
                bolSkipChecking = True
            Else
                bolSkipChecking = False
            End If
        Catch ex As Exception
            bolSkipChecking = False
        End Try
        If bolSkipChecking = False Then
            For i = 0 To arrayPayDetail.Count - 1
                paymentData = arrayPayDetail(i)
                If (paymentData.PayTypeID = POSType.PayByCreditCard) And (paymentData.CreditCardNumber = creditcardNumber) And (creditcardNumber <> "") Then
                    paymentData.PayPrice += payPrice
                    CalculatePaymentVATAndVATable(paymentData, shopVAT)
                    Return i
                End If
            Next i
        End If
        'Add New Payment Into ArrayList
        paymentData = New PaymentDetail_Data
        paymentData.PayID = NewPayTypeIDInPayDetailList(arrayPayDetail)
        paymentData.PayPrice = payPrice
        paymentData.CreditCardNumber = creditcardNumber
        paymentData.CreditCardTypeID = creditCardTypeID
        paymentData.CreditCardType = creditCardType
        paymentData.CreditCardBankID = bankID
        paymentData.CreditCardExpireMonth = creditcardExpireMonth
        paymentData.CreditCardExpireYear = creditcardExpireYear
        paymentData.CreditCardApprovalCode = creditcardApprovalCode
        arrayPayDetail.Add(paymentData)
        InsertPayTypeDetailIntoPaymentData(paymentData, dtPayType, payTypeIndex)
        CalculatePaymentVATAndVATable(paymentData, shopVAT)
        Return -1
    End Function

    Public Shared Function InsertOrUpdatePayByChequeDetailInList(ByVal arrayPayDetail As List(Of PaymentDetail_Data), _
    ByVal dtPayType As DataTable, ByVal payTypeIndex As Integer, ByVal payPrice As Decimal, ByVal chequeNumber As String, _
    ByVal bankID As Integer, ByVal chequeBankBranch As String, ByVal chequeDate As Date, ByVal shopVAT As Decimal) As Integer
        Dim i As Integer
        Dim paymentData As PaymentDetail_Data
        Dim bolSkipChecking As Boolean
        Try
            If dtPayType.Rows(payTypeIndex)("NotCheckSamePaymentDetail") = 1 Then
                bolSkipChecking = True
            Else
                bolSkipChecking = False
            End If
        Catch ex As Exception
            bolSkipChecking = False
        End Try
        If bolSkipChecking = False Then
            For i = 0 To arrayPayDetail.Count - 1
                paymentData = arrayPayDetail(i)
                If (paymentData.PayTypeID = POSType.PayByCheque) And (paymentData.ChequeNumber = chequeNumber) Then
                    paymentData.PayPrice += payPrice
                    CalculatePaymentVATAndVATable(paymentData, shopVAT)
                    Return i
                End If
            Next i
        End If
        'Add New Payment Into ArrayList
        paymentData = New PaymentDetail_Data
        paymentData.PayID = NewPayTypeIDInPayDetailList(arrayPayDetail)
        paymentData.PayPrice = payPrice
        paymentData.ChequeNumber = chequeNumber
        paymentData.ChequeDate = chequeDate
        paymentData.CreditCardBankID = bankID
        paymentData.PaidBy = chequeBankBranch
        arrayPayDetail.Add(paymentData)
        InsertPayTypeDetailIntoPaymentData(paymentData, dtPayType, payTypeIndex)
        CalculatePaymentVATAndVATable(paymentData, shopVAT)
        Return -1
    End Function

    Public Overloads Shared Function InsertOrUpdatePayByCMCashCouponAndOtherDetailInList(ByVal arrayPayDetail As List(Of PaymentDetail_Data), _
    ByVal dtPayType As DataTable, ByVal payTypeIndex As Integer, ByVal payPrice As Decimal, _
    ByVal paidByDetail As String, ByVal shopVAT As Decimal) As Integer
        Return InsertOrUpdatePayByCMCashCouponAndOtherDetailInList(arrayPayDetail, dtPayType, payTypeIndex, payPrice, paidByDetail, 0, shopVAT)
    End Function

    Public Overloads Shared Function InsertOrUpdatePayByCMCashCouponAndOtherDetailInList(ByVal arrayPayDetail As List(Of PaymentDetail_Data), _
    ByVal dtPayType As DataTable, ByVal payTypeIndex As Integer, ByVal payPrice As Decimal, _
    ByVal paidByDetail As String, ByVal paidByReasonID As Integer, ByVal shopVAT As Decimal) As Integer
        Dim i, selPayTypeID As Integer
        Dim paymentData As PaymentDetail_Data
        Dim bolSkipChecking As Boolean
        Try
            If dtPayType.Rows(payTypeIndex)("NotCheckSamePaymentDetail") = 1 Then
                bolSkipChecking = True
            Else
                bolSkipChecking = False
            End If
        Catch ex As Exception
            bolSkipChecking = False
        End Try
        If bolSkipChecking = False Then
            If dtPayType.Rows(payTypeIndex)("ConvertPayTypeTo") = 0 Then
                selPayTypeID = dtPayType.Rows(payTypeIndex)("PayTypeID")
            Else
                selPayTypeID = dtPayType.Rows(payTypeIndex)("ConvertPayTypeTo")
            End If
            For i = 0 To arrayPayDetail.Count - 1
                paymentData = arrayPayDetail(i)
                If (paymentData.PayTypeID = selPayTypeID) And (paymentData.PaidBy = paidByDetail) Then
                    paymentData.PayPrice += payPrice
                    CalculatePaymentVATAndVATable(paymentData, shopVAT)
                    Return i
                End If
            Next i
        End If
        'Add New Payment Into ArrayList
        paymentData = New PaymentDetail_Data
        paymentData.PayID = NewPayTypeIDInPayDetailList(arrayPayDetail)
        paymentData.PayPrice = payPrice
        paymentData.PaidBy = paidByDetail
        paymentData.PaidReasonID = paidByReasonID
        arrayPayDetail.Add(paymentData)
        InsertPayTypeDetailIntoPaymentData(paymentData, dtPayType, payTypeIndex)
        CalculatePaymentVATAndVATable(paymentData, shopVAT)
        Return -1
    End Function

    Public Overloads Shared Function InsertOrUpdatePayByOtherReceiptDetailInList(ByVal arrayPayDetail As List(Of PaymentDetail_Data), _
    ByVal dtPayType As DataTable, ByVal payTypeIndex As Integer, ByVal payPrice As Decimal, _
    ByVal payNote As String, ByVal shopVAT As Decimal) As Integer
        Dim paymentData As New PaymentDetail_Data
        paymentData.PayID = NewPayTypeIDInPayDetailList(arrayPayDetail)
        paymentData.PayPrice = payPrice
        paymentData.PaidBy = payNote
        arrayPayDetail.Add(paymentData)
        InsertPayTypeDetailIntoPaymentData(paymentData, dtPayType, payTypeIndex)
        CalculatePaymentVATAndVATable(paymentData, shopVAT)
        Return -1
    End Function

    Public Overloads Shared Function InsertOrUpdatePayByOtherReceiptDetailInList(ByVal arrayPayDetail As List(Of PaymentDetail_Data), _
    ByVal dtPayType As DataTable, ByVal payTypeIndex As Integer, ByVal payPrice As Decimal, ByVal shopVAT As Decimal) As Integer
        Dim strPayTypeCode As String
        If Not IsDBNull(dtPayType.Rows(payTypeIndex)("PayTypeCode")) Then
            strPayTypeCode = dtPayType.Rows(payTypeIndex)("PayTypeCode")
        Else
            strPayTypeCode = ""
        End If
        Return InsertOrUpdatePayByOtherReceiptDetailInList(arrayPayDetail, dtPayType, payTypeIndex, payPrice, strPayTypeCode, shopVAT)
    End Function

    Public Shared Function InsertOrUpdatePayBySmartcardPrepaidDetailInList(ByVal arrayPayDetail As List(Of PaymentDetail_Data), _
    ByVal dtPayType As DataTable, ByVal payTypeIndex As Integer, ByVal payPrice As Decimal, _
    ByVal cardID As Integer, ByVal cardProductLevelID As Integer, ByVal cardNo As String, _
    ByVal cardAmountLeft As Decimal, ByVal shopVAT As Decimal) As Integer
        Dim i As Integer
        Dim paymentData As PaymentDetail_Data
        Dim bolSkipChecking As Boolean
        Try
            If dtPayType.Rows(payTypeIndex)("NotCheckSamePaymentDetail") = 1 Then
                bolSkipChecking = True
            Else
                bolSkipChecking = False
            End If
        Catch ex As Exception
            bolSkipChecking = False
        End Try
        If bolSkipChecking = False Then
            For i = 0 To arrayPayDetail.Count - 1
                paymentData = arrayPayDetail(i)
                If (paymentData.PayTypeID = dtPayType.Rows(payTypeIndex)("PayTypeID")) And _
                   (paymentData.CardID = cardID) And _
                   (paymentData.CardProductLevelID = cardProductLevelID) Then
                    paymentData.PayPrice += payPrice
                    paymentData.CardCurrentAmountMoney = cardAmountLeft
                    CalculatePaymentVATAndVATable(paymentData, shopVAT)
                    Return i
                End If
            Next i
        End If
        'Add New Payment Into ArrayList
        paymentData = New PaymentDetail_Data
        paymentData.PayID = NewPayTypeIDInPayDetailList(arrayPayDetail)
        paymentData.PayPrice = payPrice
        paymentData.CardID = cardID
        paymentData.CardProductLevelID = cardProductLevelID
        paymentData.CreditCardNumber = cardNo
        paymentData.CardCurrentAmountMoney = cardAmountLeft
        arrayPayDetail.Add(paymentData)
        InsertPayTypeDetailIntoPaymentData(paymentData, dtPayType, payTypeIndex)
        CalculatePaymentVATAndVATable(paymentData, shopVAT)
        Return -1
    End Function

    Public Shared Function InsertOrUpdatePayByRedeemPointInList(ByVal arrayPayDetail As List(Of PaymentDetail_Data), _
    ByVal dtPayType As DataTable, ByVal payTypeIndex As Integer, ByVal payPrice As Decimal, _
    ByVal redeemID As Integer, ByVal redeemPoint As Decimal, ByVal afterRedeemPoint As Decimal, ByVal redeemPointPerUnit As Decimal, _
    ByVal redeemPointPerPayAmount As Decimal, ByVal redeemMemberID As Integer, _
    ByVal redeemTransID As Integer, ByVal redeemTransComID As Integer, ByVal redeemName As String, ByVal shopVAT As Decimal) As Integer
        Dim paymentData As PaymentDetail_Data
        'Add New Payment Into ArrayList
        paymentData = New PaymentDetail_Data
        paymentData.PayID = NewPayTypeIDInPayDetailList(arrayPayDetail)
        paymentData.PayPrice = payPrice
        paymentData.RedeemID = redeemID
        paymentData.RedeemMemberID = redeemMemberID
        paymentData.RedeemPointPerUnit = redeemPointPerUnit
        paymentData.RedeemPointPerPaymentAmount = redeemPointPerPayAmount
        paymentData.RedeemPoint = redeemPoint
        paymentData.AfterRedeemPoint = afterRedeemPoint
        paymentData.RedeemTransactionID = redeemTransID
        paymentData.RedeemComputerID = redeemTransComID
        paymentData.PaidBy = redeemName
        arrayPayDetail.Add(paymentData)
        InsertPayTypeDetailIntoPaymentData(paymentData, dtPayType, payTypeIndex)
        CalculatePaymentVATAndVATable(paymentData, shopVAT)
        Return -1
    End Function

End Class




















