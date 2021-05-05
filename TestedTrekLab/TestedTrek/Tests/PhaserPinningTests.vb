Imports System.Text

Imports NUnit.Framework

Imports StarTrek.Untouchables

<TestFixture> _
Public Class PhaserPinningTests
    Private game As Game
    Private context As MockGalaxy

    Const EnergyInNewGame As Integer = 10000

    <TearDown> _
    Public Sub RemoveTheMockRandomGeneratorForOtherTests_IReallyWantToRefactorThatStaticVariableSoon()
        Game.generator = New Random()
    End Sub

    <SetUp> _
    Public Sub SetUp()
        game = New Game()
        context = New MockGalaxy()
        context.SetValueForTesting("command", "phaser")
    End Sub

    <Test> _
    Public Sub PhasersFiredWithInsufficientEnergy()
        context.SetValueForTesting("amount", (EnergyInNewGame + 1).ToString())
        game.FireWeapon(context)
        Assert.AreEqual("Insufficient energy to fire phasers! || ", context.GetAllOutput())
    End Sub

    <Test> _
    Public Sub PhasersFiredWhenKlingonOutOfRange_AndEnergyExpendedAnyway()
        Dim maxPhaserRange As Integer = 4000
        Dim outOfRange As Integer = maxPhaserRange + 1
        context.SetValueForTesting("amount", "1000")
        context.SetValueForTesting("target", New MockKlingon(outOfRange))
        game.FireWeapon(context)
        Assert.AreEqual("Klingon out of range of phasers at " & outOfRange & " sectors... || ", context.GetAllOutput())
        Assert.AreEqual(EnergyInNewGame - 1000, game.EnergyRemaining())
    End Sub

    <Test> _
    Public Sub PhasersFiredKlingonDestroyed()
        Dim klingon As New MockKlingon(2000, 200)
        context.SetValueForTesting("amount", "1000")
        context.SetValueForTesting("target", klingon)
        Game.generator = New MockRandom()
        game.FireWeapon(context)
        Assert.AreEqual("Phasers hit Klingon at 2000 sectors with 400 units || Klingon destroyed! || ", context.GetAllOutput())
        Assert.AreEqual(EnergyInNewGame - 1000, game.EnergyRemaining())
        Assert.IsTrue(klingon.DeleteWasCalled())
    End Sub

    <Test> _
    Public Sub PhasersDamageOfZeroStillHits_AndNondestructivePhaserDamageDisplaysRemaining()
        Dim minimalFired As String = "0"
        Dim minimalHit As String = "1"
        context.SetValueForTesting("amount", minimalFired)
        context.SetValueForTesting("target", New MockKlingon(2000, 200))
        Game.generator = New MockRandom()
        game.FireWeapon(context)
        Assert.AreEqual("Phasers hit Klingon at 2000 sectors with " & minimalHit & " units || Klingon has 199 remaining || ", context.GetAllOutput())
        ' Isn't this also a bug?  I *ask* to fire zero, and I still hit?
        ' Acknowledge it, log it, but don't fix it yet!
    End Sub

End Class
