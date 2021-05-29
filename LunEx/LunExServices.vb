Imports System
Imports System.Collections.Generic

Namespace ITC
    Public Interface SecurityExchangeTransmissionInterface
        Function CurrentPrice(symbol As String) As Integer
    End Interface
End Namespace

Namespace LunEx
    ''' <summary>
    ''' UNTOUCHABLE!  Please do NOT change this file.
    ''' @see ITC.SecurityExchangeTransmissionInterface
    ''' </summary>
    Public Class LunExServices
        Implements ITC.SecurityExchangeTransmissionInterface
        Private Shared invisibleHand As New Random()

        Public Overridable Function CurrentPrice(symbol As String) As Integer Implements ITC.SecurityExchangeTransmissionInterface.CurrentPrice
            PauseToEmulateSendReceive()
            If invisibleHand.[Next](100) > 80 Then
                Throw New LunExServiceUnavailableException()
            End If
            Return 103 + invisibleHand.[Next](20)
        End Function

        Private Sub PauseToEmulateSendReceive()
            System.Threading.Thread.Sleep(5000)
        End Sub
    End Class
    Public Class LunExServiceUnavailableException
        Inherits Exception
        Public Overrides ReadOnly Property Message() As String
            Get
                Return "Sorry, sunspot activity today...please try again later"
            End Get
        End Property
    End Class
End Namespace
