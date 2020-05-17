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
