function PreLaunchDataGathering
{

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing


#create a form and set its attributes

$form = New-Object System.Windows.Forms.Form
$font = New-Object System.Drawing.Font("Calibri",12)
$form.Font = $font
$form.Text = 'Pre-Launch Data Gathering from Script'
$form.Size = New-Object System.Drawing.Size(600,300)
$form.StartPosition = 'CenterScreen'


#################################################################################

#Add a label
$label = New-Object System.Windows.Forms.Label
$label.Location = New-Object System.Drawing.Point(10,10)
$label.Size = New-Object System.Drawing.Size(500,40)
$label.Text = 'Please enter the information in the space below:'
$form.Controls.Add($label)

#Add a label
#$label = New-Object System.Windows.Forms.Label
#$label.Location = New-Object System.Drawing.Point(10,50)
#$label.Size = New-Object System.Drawing.Size(120,25)
#$label.Text = 'User Name'
#$form.Controls.Add($label)


#Add a textBox
#$textBox = New-Object System.Windows.Forms.TextBox
#$textBox.Location = New-Object System.Drawing.Point(150,50)
#$textBox.Size = New-Object System.Drawing.Size(250,25)
#$form.Controls.Add($textBox)


#Add a label
$label = New-Object System.Windows.Forms.Label
$label.Location = New-Object System.Drawing.Point(10,10)
$label.Size = New-Object System.Drawing.Size(500,40)
$label.Text = 'Mode of opening the app:'
$form.Controls.Add($label)


# Create a group that will contain your radio buttons


    $MyGroupBox = New-Object System.Windows.Forms.GroupBox
    $MyGroupBox.Location = '10,90'
    $MyGroupBox.size = '400,110'
    $MyGroupBox.text = "Mode"
    
    # Create the collection of radio buttons
    $RadioButton1 = New-Object System.Windows.Forms.RadioButton
    $RadioButton1.Location = '20,40'
    $RadioButton1.size = '350,25'
    $RadioButton1.Checked = $true 
    $RadioButton1.Text = "Regular Mode"
 
    $RadioButton2 = New-Object System.Windows.Forms.RadioButton
    $RadioButton2.Location = '20,70'
    $RadioButton2.size = '350,25'
    $RadioButton2.Checked = $false
    $RadioButton2.Text = "Kiosk Mode"
 
    # Add all the GroupBox controls on one line
    $MyGroupBox.Controls.AddRange(@($Radiobutton1,$RadioButton2))
 
    # Add all the Form controls on one line 
    $form.Controls.AddRange(@($MyGroupBox,$OKButton,$CancelButton,$RadioButton3))

#Add ok button

$OKButton = New-Object System.Windows.Forms.Button
$OKButton.Location = New-Object System.Drawing.Point(10,200)
$OKButton.Size = New-Object System.Drawing.Size(100,40)
$OKButton.Text = 'OK'
$OKButton.DialogResult = [System.Windows.Forms.DialogResult]::OK
$form.AcceptButton = $OKButton
$form.Controls.Add($OKButton)

#Add cancel button
$CancelButton = New-Object System.Windows.Forms.Button
$CancelButton.Location = New-Object System.Drawing.Point(120,200)
$CancelButton.Size = New-Object System.Drawing.Size(100,40)
$CancelButton.Text = 'Cancel'
$CancelButton.DialogResult = [System.Windows.Forms.DialogResult]::Cancel
$form.CancelButton = $CancelButton
$form.Controls.Add($CancelButton)


#show form
$form.Topmost = $true

#$form.Add_Shown({$textBox.Select()})
$result = $form.ShowDialog()
$result


# Check the current state of each radio button and respond accordingly

$msg= "";
$regVal="";
        if ($RadioButton1.Checked -and (!($RadioButton2.Checked)))
        {
           $msg= "Regular Mode";
           $regVal="false";
          
        }
        elseif ($RadioButton2.Checked)
        {
                $msg= "Kiosk Mode";
                $regVal="true";
        }
         
          [System.Windows.Forms.MessageBox]::Show($msg+" selected!", "Mode Selected")
           $msg     


           cd HKCU:\
           $path= 'SOFTWARE\Contoso\MyEmployees'
           $res1=New-Item -Path $path -Force
           $res2=New-ItemProperty -Path $path -Name KioskMode -Value $regVal -Force
          
                 
    if ($result -eq [System.Windows.Forms.DialogResult]::OK)
    {
        $x = $textBox.Text
        $x
    }

}


PreLaunchDataGathering


$temp = Read-Host -Prompt 'name'