using System;
using System.Drawing;
using System.Windows.Forms;

namespace AttendanceSystem
{
    public class LoginForm : Form
    {
        // ── Controls ──────────────────────────────────────────────
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnSignIn;
        private Label lblError;

        // ── Constructor ───────────────────────────────────────────
        public LoginForm()
        {
            BuildUI();
        }

        // ── UI construction ───────────────────────────────────────
        private void BuildUI()
        {
            /* Form */
            Text = "EMS - Login";
            Size = new Size(500, 400);
            MinimumSize = new Size(500, 400);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(220, 225, 235);
            Font = new Font("Segoe UI", 9f);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            /* Title */
            lblTitle = MakeLabel("EMS", 20, 30, 460, 40,
                                new Font("Segoe UI", 24f, FontStyle.Bold),
                                Color.FromArgb(60, 60, 60));

            /* Subtitle */
            lblSubtitle = MakeLabel("(Login)", 20, 65, 460, 25,
                                   new Font("Segoe UI", 14f),
                                   Color.FromArgb(100, 100, 100));

            /* Login Panel */
            var pnlLogin = new Panel
            {
                Location = new Point(50, 110),
                Size = new Size(400, 220),
                BackColor = Color.White
            };

            /* Sign in prompt */
            var lblPrompt = MakeLabel("Sign in to start your session", 20, 20, 360, 30,
                                     new Font("Segoe UI", 11f),
                                     Color.FromArgb(100, 100, 100));
            pnlLogin.Controls.Add(lblPrompt);

            /* Username Label and TextBox */
            lblUsername = MakeLabel("Username", 20, 60, 360, 20,
                                   new Font("Segoe UI", 9f),
                                   Color.FromArgb(60, 60, 60));
            pnlLogin.Controls.Add(lblUsername);

            txtUsername = new TextBox
            {
                Location = new Point(20, 82),
                Size = new Size(360, 35),
                Font = new Font("Segoe UI", 11f),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlLogin.Controls.Add(txtUsername);

            /* Password Label and TextBox */
            lblPassword = MakeLabel("Password", 20, 125, 360, 20,
                                   new Font("Segoe UI", 9f),
                                   Color.FromArgb(60, 60, 60));
            pnlLogin.Controls.Add(lblPassword);

            txtPassword = new TextBox
            {
                Location = new Point(20, 147),
                Size = new Size(360, 35),
                Font = new Font("Segoe UI", 11f),
                UseSystemPasswordChar = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlLogin.Controls.Add(txtPassword);

            /* Sign In Button */
            btnSignIn = new Button
            {
                Text = "Sign in",
                Location = new Point(20, 190),
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(52, 120, 180),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSignIn.FlatAppearance.BorderSize = 0;
            btnSignIn.Click += OnSignIn;
            pnlLogin.Controls.Add(btnSignIn);

            /* Error Label */
            lblError = MakeLabel("", 20, 350, 460, 20,
                                new Font("Segoe UI", 9f),
                                Color.Red);

            /* Add controls to form */
            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(pnlLogin);
            Controls.Add(lblError);
        }

        // ── Sign in handler ───────────────────────────────────────
        private void OnSignIn(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                lblError.Text = "Please enter a username.";
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                lblError.Text = "Please enter a password.";
                return;
            }

            // Simple validation (you can replace with actual authentication)
            if (username == "teacher" && password == "12345")
            {
                lblError.Text = "";
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                lblError.Text = "Invalid username or password.";
                txtPassword.Clear();
            }
        }

        // ── Helpers ───────────────────────────────────────────────
        private Label MakeLabel(string text, int x, int y, int w, int h,
                                Font font = null, Color? color = null)
        {
            var lbl = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, h),
                Font = font ?? this.Font,
                ForeColor = color ?? Color.Black,
                AutoSize = false
            };
            return lbl;
        }
    }
}
