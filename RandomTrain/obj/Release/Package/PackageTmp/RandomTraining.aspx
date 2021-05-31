<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RandomTraining.aspx.cs" Inherits="RandomTrain.RandomTraining1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link runat="server" rel="icon" href="/favicon.png" type="image/png" />
    <title>RanPump</title>
    <style type="text/css">
        th, td {
            padding: 15px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .ranForm {
            display: flex;
            flex-direction: column;
            font-family: Tahoma;
            color: azure;
            border: solid;
            border-width: 5px;
            border-color: dodgerblue;
            border-radius: 10px;
            background-color: cornflowerblue;
            padding: 7px;
            width: 300px;
        }

        .button {
            background-color: #52c77d; /* Green */
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            border-radius: 10px;
            margin-top: 7px;
        }

        .markButton {
            border-radius: 10px;
        }

        .table {
            background-color: cornflowerblue;
            border-radius: 10px;
            margin: 10px;
            font-family: Tahoma;
            color: #52c77d;
            background-image: url(https://i.pinimg.com/564x/a5/a2/3b/a5a23b95999b83a1848cd45717a6c320.jpg);
            background-size: cover;
            background-repeat: no-repeat;
        }

        /*body {
            background-image: url(https://i.pinimg.com/originals/d2/e3/c1/d2e3c1fb3eddcea50f08e124ec935cd2.jpg);
            background-repeat: no-repeat;
            background-size:*/ /*100% auto*/ /*cover;
        }

        @media screen and (min-width:900px) and (max-width:2000px) {
            body {
                background-image: url(https://i.pinimg.com/originals/d2/e3/c1/d2e3c1fb3eddcea50f08e124ec935cd2.jpg);
                background-repeat: no-repeat;
                background-size:*/ /*100% auto*/ /*cover;
            }
        }

        @media screen and (min-width:1px) and (max-width:900px) {
            body {
                background-image: url(https://i.redd.it/b9omhdrk6fz31.jpg);
                background-repeat: no-repeat;
                background-size:*/ /*auto 100%*/ /*cover;
            }
        }*/

        .listExcercises {
            background-color: #FFFF00;
            color: #FF0000;
        }

        .list-textbox {
            font-family: 'Comic Sans MS';
            color: azure;
        }

        .headline {
            font-family: 'Comic Sans MS';
            color: azure;
        }

        .entryField {
            border-radius: 20px;
            height: 40px;
        }
        /*tr:nth-child(even) {background-color: #f2f2f2;}*/
    </style>
</head>
<body style="background-image: url(https://i.pinimg.com/564x/5f/65/f9/5f65f9ba886bfd989c350f88a443fb93.jpg); background-size: cover; background-repeat: unset;">
    <form id="form1" runat="server">
        <asp:Panel runat="server">
            <div style="display: flex; flex-direction: row">
                <div class="ranForm">
                    <h1 class="headline">Randomizing form</h1>
                    <asp:Button Text="Get training!" ID="btnGetTraining" CssClass="button" runat="server" OnClick="btnGetTraining_Click" AutoPostBack="true" />
                    <asp:Label runat="server" ID="lbExcersizeList" CssClass="list-textbox" />
                    <asp:Table runat="server" ID="tblPlan" />
                </div>
                <div>
                    <asp:Table ID="trainingTable" runat="server" CssClass="table">
                        <asp:TableRow>
                            <asp:TableCell>Id</asp:TableCell>
                            <asp:TableCell>Name</asp:TableCell>
                            <asp:TableCell>Plan</asp:TableCell>
                            <asp:TableCell>Breaks</asp:TableCell>
                            <asp:TableCell>Muscles</asp:TableCell>
                            <asp:TableCell>Author</asp:TableCell>
                            <asp:TableCell>ExtraInfo</asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <div style="display: flex; flex-direction: row-reverse">
                        <asp:Button ID="btnBackup" runat="server" CssClass="button" Style="margin:7px" OnClick="btnBackup_Click" Text="Backup" />
                    </div>
                </div>
                <div class="ranForm">
                    <h1 class="headline">Training view</h1>
                    <asp:Label runat="server" ID="lblId" CssClass="entryField" placeholder="Id" Text="ID" />
                    <asp:TextBox runat="server" ID="tbName" CssClass="entryField" placeholder="Training name" />
                    <asp:TextBox runat="server" ID="tbPlan" CssClass="entryField" placeholder="Plan text" />
                    <asp:TextBox runat="server" ID="tbBreaks" CssClass="entryField" placeholder="Breaks duration" />
                    <asp:TextBox runat="server" ID="tbMuscles" CssClass="entryField" placeholder="Muscle groups" />
                    <asp:TextBox runat="server" ID="tbAuthor" CssClass="entryField" placeholder="Training author name" />
                    <asp:TextBox runat="server" ID="tbExtraInfo" CssClass="entryField" placeholder="Extra info about training" />
                    <asp:Button runat="server" ID="btnAddTraining" Text="Add" CssClass="button" OnClick="btnAddTraining_Click" AutoPostBack="true" />
                    <asp:Button runat="server" ID="btnEdit" Text="Edit" CssClass="button" OnClick="btnEdit_Click" AutoPostBack="true" />
                    <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="button" OnClick="btnDelete_Click" AutoPostBack="true" />
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
