using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace krestiki
{
    public partial class MainWindow : Window
    {
        private List<Button> buttons;
        private bool PlayerX = true;
        private bool GameOver = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeButtonsList();
        }

        private void InitializeButtonsList()
        {
            buttons = new List<Button>
            {
                btn1, btn2, btn3,
                btn4, btn5, btn6,
                btn7, btn8, btn9
            };
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {


            foreach (var button in buttons)
            {
                button.Content = "";
                button.IsEnabled = true;
            }
            PlayerX = true;
            GameOver = false;

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (!GameOver && button.IsEnabled)
            {
                button.Content = PlayerX ? "X" : "O";
                button.IsEnabled = false;
                if (CheckWinner())
                {
                    string winner = PlayerX ? "X" : "O";
                    MessageBox.Show($"Победил.: {winner}");
                    GameOver = true;
                }
                else if (Fullfield())
                {
                    MessageBox.Show(" Ну получается ничья (((");
                    GameOver = true;
                }
                else
                {

                    PlayerX = !PlayerX;
                    if (!PlayerX)
                    {
                        ComputerMove();
                    }
                }
            }
        }


        private bool CheckWinner()
        {

            string[] symbols = new string[9];
            for (int i = 0; i < 9; i++)
            {
                symbols[i] = buttons[i].Content.ToString();
            }


            if ((symbols[0] == symbols[1] && symbols[1] == symbols[2] && symbols[0] != "") ||
                (symbols[3] == symbols[4] && symbols[4] == symbols[5] && symbols[3] != "") ||
                (symbols[6] == symbols[7] && symbols[7] == symbols[8] && symbols[6] != "") ||
                (symbols[0] == symbols[3] && symbols[3] == symbols[6] && symbols[0] != "") ||
                (symbols[1] == symbols[4] && symbols[4] == symbols[7] && symbols[1] != "") ||
                (symbols[2] == symbols[5] && symbols[5] == symbols[8] && symbols[2] != "") ||
                (symbols[0] == symbols[4] && symbols[4] == symbols[8] && symbols[0] != "") ||
                (symbols[2] == symbols[4] && symbols[4] == symbols[6] && symbols[2] != ""))
            {

                return true;
            }

            return false;
        }


        private bool Fullfield()
        {

            foreach (var button in buttons)
            {
                if (button.Content.ToString() == "")
                {

                    return false;
                }
            }


            return true;
        }


        private void ComputerMove()
        {

            Random random = new Random();
            int randomIndex;
            do
            {
                randomIndex = random.Next(1, 10);
            } while (!buttons[randomIndex - 1].IsEnabled);

            Button button = buttons[randomIndex - 1];
            button.Content = PlayerX ? "X" : "O";
            button.IsEnabled = false;
            if (CheckWinner())
            {
                string winner = PlayerX ? "X" : "O";
                MessageBox.Show($"Победил.: {winner}");
                GameOver = true;
            }
            else if (Fullfield())
            {
                MessageBox.Show("Игра окончена. Ничья.");
                GameOver = true;
            }
            else
            {
                PlayerX = !PlayerX;
            }
        }
    }
}