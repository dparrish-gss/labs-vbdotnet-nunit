Imports Account
Imports LunExService.LunEx

<TestClass>
Public Class AccountTests
    <TestMethod>
    Public Sub AccountTotalGain_WhenUsingLunEx()
        'Given
        Dim firstLot = New Lot(100, 3000)
        Dim latestLot = New Lot(10, 400)
        Dim account = New Account.Account()
        Dim symbol = "HE3"
        ' Current price of HE3 was 42 when we wrote this...

        'When/Then
        Assert.AreEqual(1220L, account.TotalGain({firstLot, latestLot}, symbol, New LunExServices()))

    End Sub

    <TestMethod>
    Public Sub AccountTotalGain_WhenUsingInteger()
        'Given
        Dim firstLot = New Lot(100, 3000)
        Dim latestLot = New Lot(10, 400)
        Dim account = New Account.Account()

        'When/Then
        Assert.AreEqual(1220L, account.TotalGain({firstLot, latestLot}, 42))
    End Sub
End Class
