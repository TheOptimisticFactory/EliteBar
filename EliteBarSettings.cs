using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using SharpDX;

namespace EliteBar
{
    public class EliteBarSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(true);


        [Menu("Space between bars")]
        public RangeNode<int> Space { get; set; } = new RangeNode<int>(20, -100, 100);
        [Menu("X")]
        public RangeNode<int> X { get; set; } = new RangeNode<int>(0, 0, 3000);
        [Menu("Y")]
        public RangeNode<int> Y { get; set; } = new RangeNode<int>(200, 0, 3000);
        [Menu("Width")]
        public RangeNode<int> Width { get; set; } = new RangeNode<int>(100, 1, 1000);
        [Menu("Dynamic Width")]
        public ToggleNode DynamicWidth { get; set; } = new ToggleNode(false);
        [Menu("Height")]
        public RangeNode<int> Height { get; set; } = new RangeNode<int>(20, 1, 200);
        [Menu("Border color")]
        public ColorNode BorderColor { get; set; } = new ColorNode(Color.Black);

        [Menu("Refresh")]
        public ButtonNode RefreshEntities { get; set; } = new ButtonNode();
        [Menu("Offset X for text")]
        public RangeNode<int> StartTextX { get; set; } = new RangeNode<int>(0, -50, 350);
        [Menu("Offset Y for text")]
        public RangeNode<int> StartTextY { get; set; } = new RangeNode<int>(0, -50, 350);


        [Menu("Rare monster color")]
        public ColorNode RareMonster { get; set; } = new ColorNode(Color.Yellow);
        [Menu("Unique monster color")]
        public ColorNode UniqueMonster { get; set; } = new ColorNode(Color.Orange);
        [Menu("Show rare monster")]
        public ToggleNode ShowRare { get; set; } = new ToggleNode(true);
        [Menu("Show unique monster")]
        public ToggleNode ShowUnique { get; set; } = new ToggleNode(true);


        [Menu("Text Color")]
        public ColorNode TextColor { get; set; } = new ColorNode(Color.White);
        [Menu("Show only Monster Name and %Hp Text")]
        public ToggleNode LimitText { get; set; } = new ToggleNode(true);


        [Menu("Use imgui for draw")]
        public ToggleNode UseImguiForDraw { get; set; } = new ToggleNode(false);
        [Menu("BG for imgui")]
        public ColorNode ImguiDrawBg { get; set; } = new ColorNode(Color.Black);
        public ToggleNode Debug { get; set; } = new ToggleNode(false);
    }
}
