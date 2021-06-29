Imports LunExService.LunEx
Public Class Account

    Public Function TotalGain(ByVal lots As Lot(), ByVal currentPrice As Long) As Long
        Dim total As Long = 0
        For Each lot As Lot In lots
            total += GainForLot(lot, currentPrice)
        Next
        Return total
    End Function

    Private Function GainForLot(ByVal lot As Lot, ByVal currentPrice As Long) As Long
        Return lot.GainAt(currentPrice)
    End Function
End Class
