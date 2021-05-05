Public Class MockRandom
    Inherits Random
    Public Overrides Function [Next](maxValue As Integer) As Integer
        ' always return 1/2 max:  It isn't random, and that's the point!
        Return maxValue \ 2
    End Function
End Class
