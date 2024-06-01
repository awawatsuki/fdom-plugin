using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using SamplePlugin.Manager;

namespace SamplePlugin.Windows;

public class MainWindow : Window, IDisposable
{
    private Plugin Plugin;
    private DebtManager DebtManager;
    private string buf = string.Empty;
    public MainWindow(Plugin plugin, DebtManager debtmanager)
        : base("Debt Plugin##00", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(300, 400),
            MaximumSize = new Vector2(300,400)
        };
        Plugin = plugin;
        DebtManager = debtmanager;
    }

    public void Dispose() { }

    public override void Draw()
    {
        Vector2 pos;
        //-1
        pos = new Vector2(45, 75);
        ImGui.SetCursorPos(pos);
        if (ImGui.Button("-1"))
        {
            DebtManager.incrementInterest(-1);
        }

        //+1
        pos = new Vector2(135, 75);
        ImGui.SetCursorPos(pos);
        if (ImGui.Button("+1"))
        {
            DebtManager.incrementInterest(1);
        }

        //Interest Rate Descriptor
        pos = new Vector2(50, 50);
        ImGui.SetCursorPos(pos);
        ImGui.Text("Interest Rate");

        //Actual Rate and %
        pos = new Vector2(95, 78);
        ImGui.SetCursorPos(pos);
        ImGui.Text(DebtManager.interestRate.ToString() + "%%");

        //+5
        pos = new Vector2(170, 75);
        ImGui.SetCursorPos(pos);
        if (ImGui.Button("+5"))
        {
            DebtManager.incrementInterest(5);
        }

        //-5
        pos = new Vector2(15, 75);
        ImGui.SetCursorPos(pos);
        if (ImGui.Button("-5"))
        {
            DebtManager.incrementInterest(-5);
        }

        //End Date Descriptor
        pos = new Vector2(250, 50);
        ImGui.SetCursorPos(pos);
        ImGui.Text("Enter end date.");

        //End Date Input
        pos = new Vector2(250, 75);
        ImGui.SetCursorPos(pos);
        ImGui.PushItemWidth(130);
        ImGui.InputTextWithHint("##inputdate", "MM/dd/yyyy", ref buf, (uint)"MM/dd/yyyy".Length);
        ImGui.PopItemWidth();
    }
}
