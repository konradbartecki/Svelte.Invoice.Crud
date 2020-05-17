import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Invoice } from './Interfaces/Invoice';
//import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

//@Component({
//  selector: 'edit-invoice-dialog',
//  templateUrl: 'edit.dialog.component.html',
//})
//export class EditDialogComponent {

//  constructor(
//    public dialogRef: MatDialogRef<EditDialogComponent>,
//    @Inject(MAT_DIALOG_DATA) public data) {}

//  onNoClick(): void {
//    this.dialogRef.close();
//  }

//}


@Component({
  selector: 'app-home',
  templateUrl: './invoice.home.component.html'
})
export class HomeComponent {
  public invoices: Invoice[];
  public columnsToDisplay = ['id', 'clientName', 'sellDate', 'issueDate', 'payTime', 'paymentType', 'isPaid', 'netTotal', 'grossTotal', 'actions'];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, /*public dialog: MatDialog*/) {
    http.get<Invoice[]>(baseUrl + '/invoices').subscribe(result => {
      this.invoices = result;
    }, error => console.error(error));
  }

  public openEditDialog(invoice) {
      
  }

  //public openEditDialog(invoice) {
  //  console.log("got invoice");
  //  console.log(invoice);
  //  const dialogRef = this.dialog.open(EditDialogComponent, {
  //    width: 'auto',
  //    data: { invoice }
  //  });
  //  dialogRef.afterClosed().subscribe(result => {
  //    console.log('The dialog was closed');
  //    var editedInvoice = result;
  //  });
  //};

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




