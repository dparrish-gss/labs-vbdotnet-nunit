Public Class MockKlingon
    Inherits Klingon
    Private overrideDistance As Integer
    Private deleteCalled As Boolean = False

    Public Sub New(distance As Integer)
        overrideDistance = distance
    End Sub

    Public Sub New(distance As Integer, energy As Integer)
        overrideDistance = distance
        SetEnergy(energy)
    End Sub

    Public Overrides Function Distance() As Integer
        Return overrideDistance
    End Function

    Public Overrides Sub Delete()
        deleteCalled = True
    End Sub

    Friend Function DeleteWasCalled() As Boolean
        Return deleteCalled
    End Function
End Class
