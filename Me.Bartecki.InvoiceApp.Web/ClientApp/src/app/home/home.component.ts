import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  public invoices: Invoice[];
  public columnsToDisplay = ['id', 'clientName', 'sellDate', 'issueDate', 'payTime', 'paymentType', 'isPaid', 'netTotal', 'grossTotal', 'actions'];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, public dialog: MatDialog) {
    http.get<Invoice[]>(baseUrl + '/invoices').subscribe(result => {
      this.invoices = result;
    }, error => console.error(error));
  }

  public openEditDialog(invoice) {
    const dialogRef = this.dialog.open(EditDialogComponent, {
      width: '250px',
      data: { invoice }
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      var editedInvoice = result;
    });
  };

  //public openDeleteDialog(id : number) {
  //  const dialogRef = this.dialog.open(DialogOverviewExampleDialog, {
  //    width: '250px',
  //    data: { name: this.name, animal: this.animal }
  //  });
  //  dialogRef.afterClosed().subscribe(result => {
  //    console.log('The dialog was closed');
  //    this.animal = result;
  //  });
  //}
}

@Component({
  selector: 'edit-invoice-dialog',
  templateUrl: 'edit.dialog.component.html',
})
export class EditDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<EditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Invoice) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

}

//@Component({
//  selector: 'dialog-overview-example-dialog',
//  templateUrl: 'dialog-overview-example-dialog.html',
//})
//export class DialogOverviewExampleDialog {

//  constructor(
//    public dialogRef: MatDialogRef<DialogOverviewExampleDialog>,
//    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

//  onNoClick(): void {
//    this.dialogRef.close();
//  }

//}

interface Client {
  id: number;
  fullName: string;
  taxId: string;
  country: string;
  address: string;
  postalCode: string;
}

interface InvoiceRow {
  id: number;
  itemName: string;
  unitName: string;
  unitPrice: number;
  amount: number;
  vatRatePercent: number;
}

interface Invoice {
  id: number;
  year: number;
  month: number;
  clientName: string;
  sellDate: Date;
  issueDate: Date;
  paytime: number;
  paymentType: PaymentType;
  isPaid: boolean;
  netTotal: number;
  grossTotal: number;
}


enum PaymentType {
  Unknown = 0,
  Cash,
  Transfer,
  CreditCard
}
