using System.Media;

namespace Sound
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }
    private void button1_click(object sender, EventArgs e){
      SoundPlayer splayer = new SoundPlayer("")
      splayer.Play();
    }
  }
}
