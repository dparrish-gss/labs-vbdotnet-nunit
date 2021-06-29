Public Class Lot
    Private Shares As Integer
    Private Cost As Integer

    Sub New(shares As Integer, cost As Integer)
        Me.Shares = shares
        Me.Cost = cost
    End Sub

    Public Function GainAt(ByVal currentPrice As Double) As Long
        Return (Shares * currentPrice) - Cost
    End Function
End Class