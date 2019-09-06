using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {

        int[,] field = new int[9, 9];
        
        public MainWindow()
        {
            InitializeComponent();
            
            Random rand = new Random();
            int k = 0;
            while (k != 9)
            {
                int x = rand.Next(0, 9);
                int y = rand.Next(0, 9);
                if (field[x, y] == -1)
                {
                    x = rand.Next(0, 9);
                    y = rand.Next(0, 9);
                }
                else
                {
                    field[x, y] = -1;
                    k += 1;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (field[i, j] != -1)
                    {
                        for (int ii = -1; ii < 2; ii++)
                        {
                            for (int jj = -1; jj < 2; jj++)
                            {
                                int ai = i + ii;
                                int aj = j + jj;
                                if (ai >= 0 & ai < 9 & aj >= 0 & aj < 9)
                                {
                                    if (field[ai, aj] == -1)
                                    {
                                        field[i, j] += 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            
            foreach (UIElement c in fiel.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                    ((Button)c).PreviewMouseRightButtonUp += Button_PreviewMouseRightButtonUp;
                }
            }

            TextBlock[,] Text = new TextBlock[9, 9] { { textBlock00, textBlock01, textBlock02, textBlock03, textBlock04, textBlock05, textBlock06, textBlock07, textBlock08,},
                                                      { textBlock10, textBlock11, textBlock12, textBlock13, textBlock14, textBlock15, textBlock16, textBlock17, textBlock18,},
                                                      { textBlock20, textBlock21, textBlock22, textBlock23, textBlock24, textBlock25, textBlock26, textBlock27, textBlock28,},
                                                      { textBlock30, textBlock31, textBlock32, textBlock33, textBlock34, textBlock35, textBlock36, textBlock37, textBlock38,},
                                                      { textBlock40, textBlock41, textBlock42, textBlock43, textBlock44, textBlock45, textBlock46, textBlock47, textBlock48,},
                                                      { textBlock50, textBlock51, textBlock52, textBlock53, textBlock54, textBlock55, textBlock56, textBlock57, textBlock58,},
                                                      { textBlock60, textBlock61, textBlock62, textBlock63, textBlock64, textBlock65, textBlock66, textBlock67, textBlock68,},
                                                      { textBlock70, textBlock71, textBlock72, textBlock73, textBlock74, textBlock75, textBlock76, textBlock77, textBlock78,},
                                                      { textBlock80, textBlock81, textBlock82, textBlock83, textBlock84, textBlock85, textBlock86, textBlock87, textBlock88,}};
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (field[i, j] == -1)
                    {
                        Text[i, j].Text = "*";
                    }
                    else
                        if (field[i, j] == 0)
                    {
                        Text[i, j].Text = " ";
                    }
                    else
                    {
                        Text[i, j].Text = Convert.ToString(field[i, j]);
                    }
                }
            }

        }


        private void Button_Click(Object sender, RoutedEventArgs e)
        {
            Button[,] Field = new Button[9, 9] { { button00, button01, button02, button03, button04, button05, button06, button07, button08},
                                                 { button10, button11, button12, button13, button14, button15, button16, button17, button18},
                                                 { button20, button21, button22, button23, button24, button25, button26, button27, button28},
                                                 { button30, button31, button32, button33, button34, button35, button36, button37, button38},
                                                 { button40, button41, button42, button43, button44, button45, button46, button47, button48},
                                                 { button50, button51, button52, button53, button54, button55, button56, button57, button58},
                                                 { button60, button61, button62, button63, button64, button65, button66, button67, button68},
                                                 { button70, button71, button72, button73, button74, button75, button76, button77, button78},
                                                 { button80, button81, button82, button83, button84, button85, button86, button87, button88}};
            void OpenCells(int line, int column)
            {
                for (int li = -1; li < 2; li++)
                {
                    for (int co = -1; co < 2; co++)
                    {
                        int lin = line + li;
                        int col = column + co;
                        if (0 <= lin & lin < 9 & 0 <= col & col < 9)
                        {
                            if (field[lin, col] != -1 & field[lin, col] != 0)
                            {
                                Field[lin, col].Visibility = Visibility.Hidden;

                            }
                            if (field[lin, col] == 0 & (lin != line || col != column) & Field[lin, col].Visibility == Visibility.Visible)
                            {
                                Field[lin, col].Visibility = Visibility.Hidden;
                                OpenCells(lin, col);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Field[i, j] == sender)
                    {
                        Field[i, j].Visibility = Visibility.Hidden;
                        if (field[i, j] == -1)
                        {
                            Total.Text = "GAME OVER";
                        }
                        else if (field[i, j] == 0)
                        {
                            OpenCells(i, j);
                        }

                    }
                }
            }
            if ((string)((Button)e.OriginalSource).Content == "Restart")
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            int s = 0;
            for (int str = 0; str < 9; str++)
            {
                for (int st = 0; st < 9; st++)
                {
                    if (field[str, st] != -1 & Field[str, st].Visibility == Visibility.Hidden)
                    {
                        s++;
                        if (s == 72)
                            Total.Text = "You're winner!";

                    }
                }
            }
        }
        
        
                    
        private void Button_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button[,] FieldR = new Button[9, 9] { { button00, button01, button02, button03, button04, button05, button06, button07, button08},
                                                 { button10, button11, button12, button13, button14, button15, button16, button17, button18},
                                                 { button20, button21, button22, button23, button24, button25, button26, button27, button28},
                                                 { button30, button31, button32, button33, button34, button35, button36, button37, button38},
                                                 { button40, button41, button42, button43, button44, button45, button46, button47, button48},
                                                 { button50, button51, button52, button53, button54, button55, button56, button57, button58},
                                                 { button60, button61, button62, button63, button64, button65, button66, button67, button68},
                                                 { button70, button71, button72, button73, button74, button75, button76, button77, button78},
                                                 { button80, button81, button82, button83, button84, button85, button86, button87, button88}};

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (FieldR[i, j] == sender)
                    {
                        FieldR[i, j].Content = "!";
                        
                    }

                }
            }
        }
        
    }
}
