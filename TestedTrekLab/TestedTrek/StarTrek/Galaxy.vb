Imports StarTrek.Untouchables

''' <summary>
''' A simple Proxy/wrapper class for Untouchables.WebGadget,
''' allowing us to create, modify, and subclass (or mock) Galaxy.
''' Note that this object currently has no tests, because it does nothing but delegate,
''' and the delegation is not unit-testable.
''' Note also that it is production code.
''' </summary>
Public Class Galaxy
    Private webContext As WebGadget

    Public Sub New(webContext As WebGadget)
        Me.webContext = webContext
    End Sub

    Public Overridable Function Parameter(parameterName As String) As String
        Return webContext.Parameter(parameterName)
    End Function

    Public Overridable Function Variable(variableName As String) As Object
        Return webContext.Variable(variableName)
    End Function

    Public Overridable Sub WriteLine(message As String)
        webContext.WriteLine(message)
    End Sub
End Class
