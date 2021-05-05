Imports System.Collections.Generic
Imports System.Text

Imports NUnit.Framework

Imports StarTrek.Untouchables

<TestFixture> _
Public Class PhotonPinningTests
    Private game As Game
    Private context As MockGalaxy

    <TearDown> _
    Public Sub RemoveTheMockRandomGeneratorForOtherTests_IReallyWantToRefactorThatStaticVariableSoon()
        Game.generator = New Random()
    End Sub

    <SetUp> _
    Public Sub SetUp()
        game = New Game()
        context = New MockGalaxy()
        context.SetValueForTesting("command", "photon")
    End Sub

    <Test> _
    Public Sub NotifiedIfNoTorpedoesRemain()
        game.Torpedoes = 0
        context.SetValueForTesting("target", New MockKlingon(2000, 200))
        game.FireWeapon(context)
        Assert.AreEqual("No more photon torpedoes! || ", context.GetAllOutput())
    End Sub

    <Test> _
    Public Sub TorpedoMissesDueToRandomFactors()
        Dim distanceWhereRandomFactorsHoldSway As Integer = 2500
        context.SetValueForTesting("target", New MockKlingon(distanceWhereRandomFactorsHoldSway, 200))
        Game.generator = New MockRandom()
        ' without this the test would often fail
        game.FireWeapon(context)
        Assert.AreEqual("Torpedo missed Klingon at 2500 sectors... || ", context.GetAllOutput())
        Assert.AreEqual(7, game.Torpedoes)
    End Sub

    <Test> _
    Public Sub TorpedoMissesDueToDistanceAndCleverKlingonEvasiveActions()
        Dim distanceWhereTorpedoesAlwaysMiss As Integer = 3500
        context.SetValueForTesting("target", New MockKlingon(distanceWhereTorpedoesAlwaysMiss, 200))
        game.FireWeapon(context)
        Assert.AreEqual("Torpedo missed Klingon at 3500 sectors... || ", context.GetAllOutput())
        Assert.AreEqual(7, game.Torpedoes)
    End Sub

    <Test> _
    Public Sub TorpedoDestroysKlingon()
        Dim klingon As New MockKlingon(500, 200)
        context.SetValueForTesting("target", klingon)
        Game.generator = New MockRandom()
        game.FireWeapon(context)
        Assert.AreEqual("Photons hit Klingon at 500 sectors with 825 units || Klingon destroyed! || ", context.GetAllOutput())
        Assert.AreEqual(7, game.Torpedoes)
        Assert.IsTrue(klingon.DeleteWasCalled())

    End Sub

    <Test> _
    Public Sub TorpedoDamagesKlingon()
        context.SetValueForTesting("target", New MockKlingon(500, 2000))
        Game.generator = New MockRandom()
        game.FireWeapon(context)
        Assert.AreEqual("Photons hit Klingon at 500 sectors with 825 units || Klingon has 1175 remaining || ", context.GetAllOutput())
        Assert.AreEqual(7, game.Torpedoes)
    End Sub

End Class
