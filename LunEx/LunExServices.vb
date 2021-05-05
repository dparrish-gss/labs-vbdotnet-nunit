Imports System.Collections.Generic

Namespace ITC
    Public Interface SecurityExchangeTransmissionInterface
        Function CurrentPrice(symbol As String) As Double
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

        Public Overridable Function CurrentPrice(symbol As String) As Double Implements ITC.SecurityExchangeTransmissionInterface.CurrentPrice
            pause()
            If invisibleHand.[Next](100) > 80 Then
                Throw New LunExServiceUnavailableException()
            End If
            Dim randomPrice As Double = 42.0 + (invisibleHand.NextDouble() * 2.1)
            Return truncate(randomPrice)
        End Function

        Private Sub pause()
            System.Threading.Thread.Sleep(5000)
        End Sub

        Private Function truncate(original As Double) As Double
            Dim originalAsString As String = original.ToString()
            Dim truncatedString As String = originalAsString.Substring(0, 7)
            Return [Double].Parse(truncatedString)
        End Function
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
