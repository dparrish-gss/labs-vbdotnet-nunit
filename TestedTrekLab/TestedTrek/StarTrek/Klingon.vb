Public Class Klingon
    Private _distance As Integer
    Private _energy As Integer

    Public Sub New()
        Dim x As New Random()
        _distance = 100 + x.[Next](4000)

        _energy = 1000 + x.[Next](2000)
    End Sub

    Public Overridable Function Distance() As Integer
        Return _distance
    End Function

    Public Overridable Function GetEnergy() As Integer
        Return _energy
    End Function

    Public Sub TakeDamage(amount As Integer)
        _energy -= amount
    End Sub

    Public Sub SetEnergy(ByVal e As Integer)
        _energy = e
    End Sub

    Public Overridable Sub Delete()
    End Sub

End Class
