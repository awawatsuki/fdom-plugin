using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace SamplePlugin.Windows;

public class MainWindow : Window, IDisposable
{
    private Plugin Plugin;
    public static Int32 interestRate = 0;
    public static DateTime endDate;
    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public MainWindow(Plugin plugin)
        : base("Debt Plugin##00", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(400, 400),
            MaximumSize = new Vector2(500,500)
        };
        Plugin = plugin;
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
            incrementInterest(-1);
        }
        //+1
        pos = new Vector2(135, 75);
        ImGui.SetCursorPos(pos);
        if (ImGui.Button("+1"))
        {
            incrementInterest(1);
        }
        //Interest Rate Descriptor
        pos = new Vector2(50, 50);
        ImGui.SetCursorPos(pos);
        ImGui.Text("Interest Rate");
        //Actual Rate and %
        pos = new Vector2(95, 78);
        ImGui.SetCursorPos(pos);
        ImGui.Text(interestRate.ToString() + "%%");
        //+5
        pos = new Vector2(170, 75);
        ImGui.SetCursorPos(pos);
        if (ImGui.Button("+5"))
        {
            incrementInterest(5);
        }
        //-5
        pos = new Vector2(15, 75);
        ImGui.SetCursorPos(pos);
        if (ImGui.Button("-5"))
        {
            incrementInterest(-5);
        }

        //End Date Descriptor
        pos = new Vector2(250, 50);
        ImGui.SetCursorPos(pos);
        ImGui.Text("Enter end date in MM/dd/yyyy format.");

        //End Date Input
        pos = new Vector2(250, 75);
        ImGui.SetCursorPos(pos);
        ImGui.PushItemWidth(130);
        string dateplh = "";
        ImGui.InputTextWithHint("##inputdate", "MM/dd/yyyy", ref dateplh, (uint)"MM/dd/yyyy".Length);
        ImGui.PopItemWidth();
    }

    private void incrementInterest(int i)
    {
        interestRate += i;
    }

    private bool dateValid(string dateinput)
    {
        DateTime temp;
        if (DateTime.TryParse(dateinput, out temp))
        {
            return true;
        }
        return false;
    }
}
