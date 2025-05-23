using MakeYourRestaurant.Model;
using MakeYourRestaurant.View;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MakeYourRestaurant___Main.Controller;

namespace MakeYourRestaurant.Controller
{
    public class Controller
    {
        Form1 f;
        Repository r;

        bool userFilled;
        bool passFilled;

        public Controller()
        {
            f = new Form1();
            r = new Repository();

            InitListeners();
            f.buttonSignIn.Enabled = false;

            f.imageBg.Paint += (s, e) =>
            {
                var img = Image.FromFile("Assets/loginpic.png");
                var panel = (Panel)s;

                // scale so the image's height exactly matches the panel:
                float scale = (float)panel.Height / img.Height;

                // scaled size
                int w = (int)(img.Width * scale);
                int h = panel.Height;  // exactly fits

                // center horizontally (we'll crop left/right equally)
                var dest = new Rectangle(
                    (panel.Width - w) / 2,  // negative if w > panel.Width (cropping)
                    0,
                    w,
                    h
                );
                e.Graphics.DrawImage(img, dest);
            };
            f.imageBg.BackgroundImage = null;

            f.FormBorderStyle = FormBorderStyle.FixedSingle;

            Application.Run(f);
        }

        private void InitListeners()
        {
            f.buttonSignIn.Click += ButtonSignIn_Click;
            f.textBoxUsername.TextChanged += TextBoxUsername_TextChanged;
            f.textBoxPassword.TextChanged += TextBoxPassword_TextChanged;
            f.linkLabelNoAccount.LinkClicked += LinkLabelNoAccount_LinkClicked;
        }

        private void TextBoxUsername_TextChanged(object sender, EventArgs e)
        {
            userFilled = !string.IsNullOrWhiteSpace(f.textBoxUsername.Text);
            ToggleLoginButton();
        }

        private void TextBoxPassword_TextChanged(object sender, EventArgs e)
        {
            passFilled = !string.IsNullOrWhiteSpace(f.textBoxPassword.Text);
            ToggleLoginButton();
        }

        private void ToggleLoginButton()
        {
            f.buttonSignIn.Enabled = userFilled && passFilled;
        }

        private void LinkLabelNoAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // This will pop up the Windows "Open with" dialog for your mailto: URI
                var psi = new ProcessStartInfo
                {
                    FileName = "rundll32.exe",
                    Arguments = "shell32.dll,OpenAs_RunDLL mailto:MakeYourRestaurant@gmail.com",
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not open email chooser: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void ButtonSignIn_Click(object sender, EventArgs e)
        {
            string username = f.textBoxUsername.Text.Trim();
            string password = f.textBoxPassword.Text.Trim();

            System.Diagnostics.Debug.WriteLine($"Trying login: {username}/{password}");

            bool existsUser = r.GetEmployee(username, password);

            if (existsUser)
            {
                Employee emp = r.GetEmployeeInfo(username, password);

                if (emp != null)
                {
                    f.Hide();
                    new ControllerMain(emp.RestaurantId); // Pass full emp if needed
                }
                else
                {
                    MessageBox.Show("User data could not be loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("The user or password entered are wrong.", "Wrong credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
