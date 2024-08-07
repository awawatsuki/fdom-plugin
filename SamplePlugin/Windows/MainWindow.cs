using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Client.System.Input;
using ImGuiNET;
using SamplePlugin.Manager;

namespace SamplePlugin.Windows;

public class MainWindow : Window, IDisposable
{
    private Plugin Plugin;
    private DebtManager debtManager;
    private string dateInput = string.Empty;
    private string amountInput = string.Empty;
    private string keyOutput = string.Empty;
    private string keyCopied = string.Empty;
    public MainWindow(Plugin plugin, DebtManager debtmanager)
        : base("Debt Plugin##00", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(300, 400),
            MaximumSize = new Vector2(300,400)
        };
        Plugin = plugin;
        debtManager = debtmanager;
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
            debtManager.incrementInterest(-1);
        }

        //+1
        pos = new Vector2(135, 75);
        ImGui.SetCursorPos(pos);
        if (ImGui.Button("+1"))
        {
            debtManager.incrementInterest(1);
        }

        //Interest Rate Descriptor
        pos = new Vector2(40, 50);
        ImGui.SetCursorPos(pos);
        ImGui.Text("Set interest rate.");

        //Actual Rate and %
        pos = new Vector2(95, 78);
        ImGui.SetCursorPos(pos);
        ImGui.Text(DebtManager.interestRate.ToString() + "%%");

        //+5
        pos = new Vector2(170, 75);
        ImGui.SetCursorPos(pos);
        if (ImGui.Button("+5"))
        {
            debtManager.incrementInterest(5);
        }

        //-5
        pos = new Vector2(15, 75);
        ImGui.SetCursorPos(pos);
        if (ImGui.Button("-5"))
        {
            debtManager.incrementInterest(-5);
        }

        //End Date Descriptor
        pos = new Vector2(255, 50);
        ImGui.SetCursorPos(pos);
        ImGui.Text("Set end date.");

        //End Date Input
        pos = new Vector2(250, 75);
        ImGui.SetCursorPos(pos);
        ImGui.PushItemWidth(130);
        ImGui.InputTextWithHint("##inputdate", "MM/dd/yyyy", ref dateInput, (uint)"MM/dd/yyyy".Length);
        ImGui.PopItemWidth();

        //Gil Amount Descriptor
        pos = new Vector2(150, 125);
        ImGui.SetCursorPos(pos);
        ImGui.Text("Set Gil target.");

        //Gil Amount Input
        pos = new Vector2(150, 150);
        ImGui.SetCursorPos(pos);
        ImGui.PushItemWidth(130);
        ImGui.InputTextWithHint("##gilamount", "999,999,999", ref amountInput, (uint)"999,999,999".Length);
        ImGui.PopItemWidth();

        pos = new Vector2(100, 200);
        ImGui.SetCursorPos(pos);
        if (ImGui.Button("Generate Key and Copy"))
        {
            if (DebtManager.interestRate >= 0 && dateInput != null && amountInput != null)
            {
                var gilAmount = amountInput.Replace(",", string.Empty);
                keyOutput = DebtManager.generateKey(DebtManager.interestRate, DateTime.Parse(dateInput), int.Parse(gilAmount));
                ImGui.SetClipboardText(keyOutput);
                keyCopied = "Key successfully copied to clipboard";
                DebtManager.interestRate = 0;
                dateInput = string.Empty;
                amountInput = string.Empty;
            }
            else
            {
                keyCopied = string.Empty;
            }
        }

        pos = new Vector2(50, 250);
        ImGui.SetCursorPos(pos);
        ImGui.Text(keyCopied);

    }
}
