    namespace Calculator
    {

    public partial class Calculator : Form
    {
        Label labelDisplay;
        Label labelExpression;
        string currentExpression = "";
        bool isError = false;

        public Calculator()
        {
            InitializeComponent();
            labelDisplay = new Label();
            labelDisplay.Text = "";
            labelDisplay.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            labelDisplay.Size = new Size(298, 50); 
            labelDisplay.TextAlign = ContentAlignment.MiddleRight; // Right-align the text
            labelDisplay.Anchor = AnchorStyles.Right;
            labelDisplay.BackColor = Color.Transparent;
            panel2.Controls.Add(labelDisplay);


            // Expression display (top)
            labelExpression = new Label();
            labelExpression.Text = "";
            labelExpression.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            labelExpression.Size = new Size(298, 25); 
            labelExpression.TextAlign = ContentAlignment.MiddleRight; // Align text to the right
            labelExpression.BackColor = Color.Transparent; 
                                                         
            labelExpression.Location = new Point(0, 5); 
            smallPanel.Controls.Add(labelExpression);

        }
        //NUMERIC BUTTONS
        private void button7_Click(object sender, EventArgs e)
        {
            if (isError) return;
            labelDisplay.Text += "7";
        }
      

        private void button6_Click(object sender, EventArgs e)
        {
            if (isError) return;
            labelDisplay.Text += "8";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (isError) return;
            labelDisplay.Text += "9";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (isError) return;
            labelDisplay.Text += "4";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (isError) return;
            labelDisplay.Text += "5";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (isError) return;
            labelDisplay.Text += "6";
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (isError) return;
            labelDisplay.Text += "1";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (isError) return;
            labelDisplay.Text += "2";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (isError) return;
            labelDisplay.Text += "3";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (isError) return;
            labelDisplay.Text += "0";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (isError) return;
            labelDisplay.Text += "00";
        }
        // Decimal Button .
        private void button18_Click(object sender, EventArgs e)
        {
            if (isError) return;
            // Only add decimal if the current number doesn't already have one
            if (!labelDisplay.Text.Contains("."))
            {
                if (string.IsNullOrEmpty(labelDisplay.Text))
                    labelDisplay.Text = "0."; // If empty, start with 0.
                else
                    labelDisplay.Text += ".";
            }
        }
        //Clear Button
        private void button3_Click(object sender, EventArgs e)
        {
            labelDisplay.Text = "";
            labelExpression.Text = "";
            currentExpression = "";
            isError = false; // Reset error flag    
        }
        //Modulus Button %
        private void button2_Click(object sender, EventArgs e)
        {
            if (isError || string.IsNullOrWhiteSpace(labelDisplay.Text))
                return;

            try
            {
                double currentValue = Convert.ToDouble(labelDisplay.Text);

                // If no prior expression, just treat it as normal percent (divide by 100)
                if (string.IsNullOrEmpty(currentExpression))
                {
                    currentValue = currentValue / 100;
                }
                else
                {
                    // Extract the last number from the current expression (before the last operator)
                    string expr = currentExpression;

                    // Find last operator position
                    int lastOpPos = expr.LastIndexOfAny(new char[] { '+', '-', '*', '/' });
                    if (lastOpPos >= 0)
                    {
                        // Get base value (before last operator)
                        string baseExpr = expr.Substring(0, lastOpPos);

                        double baseValue = Convert.ToDouble(new System.Data.DataTable().Compute(baseExpr, null));

                        // Calculate percentage of the baseValue
                        currentValue = baseValue * currentValue / 100;
                    }
                    else
                    {
                        // If no operator, percentage is just value/100
                        currentValue = currentValue / 100;
                    }
                }

              
                labelDisplay.Text = currentValue.ToString();

             

            }
            catch
            {
                labelDisplay.Text = "Error";
                labelExpression.Text = "";
                currentExpression = "";
                isError = true;
            }
        }
        // / Button division
        private void button1_Click(object sender, EventArgs e)
        {
            currentExpression += labelDisplay.Text + "/";
            labelExpression.Text = currentExpression;
            labelDisplay.Text = "";
        }
        // * Button Multiplication
        private void button4_Click(object sender, EventArgs e)
        {
            currentExpression += labelDisplay.Text + "*";
            labelExpression.Text = currentExpression;
            labelDisplay.Text = "";
        }
        //Minus Button
        private void button8_Click(object sender, EventArgs e)
        {

            currentExpression += labelDisplay.Text + "-";
            labelExpression.Text = currentExpression;
            labelDisplay.Text = "";
        }
        // + Button
        private void button20_Click(object sender, EventArgs e)
        {
            currentExpression += labelDisplay.Text + "+";
            labelExpression.Text = currentExpression;
            labelDisplay.Text = ""; // prepare for next number
        }
        //Equal Button
        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                currentExpression += labelDisplay.Text;
                labelExpression.Text = currentExpression;

                var result = new System.Data.DataTable().Compute(currentExpression, null);
                labelDisplay.Text = result.ToString();

                isError = false; // Reset error flag if success
                // Reset expression after showing result
                currentExpression = "";
            }
            catch
            {
                labelDisplay.Text = "Error";
                currentExpression = "";
                labelExpression.Text = "";
                isError = true; // Block inputs
            }
        }
    }
}
