
using System.Drawing;
using System.Windows.Forms;

namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    public static class CustomText
    {
        public static void ColorText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
