# RequestForm

A simple web request form where users can input some details about the new user, select drives needed and software needed and send an email to the administrator with this request. 

This simplifies the way new users can be created, instead of paper, pdf's, etc it is a simple web form. 


# Pages
## IP/index
The main page is the form itself, it is what users would see when they go to the web page. 

## IP/editdrives
This is the page that allows the administrator to add, edit or delete drives which are shown on the form. 

## IP/editsoftware
This is the page that allows the administrator to add, edit or delete software which is displayed on the form. 

# Configuration
To configure email you will need to edit the appsettings.json file. 
This section
  "EmailSMTPConfig": {
    "Username": "%username%",
    "Password": "%password%",
    "Server": "%mail server ip or hostname%",
    "Port": "%port%",
    "EnableSSL": "true",
    "EmailTo": "%Email address to send to%",
    "EmailToNickName": "%nickname of the sending to email address%",
    "EmailFrom": "%Which account is being mailed from%",
    "EmailSubject": "%Subject of email%"
  }