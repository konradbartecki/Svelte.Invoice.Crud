import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'invoice-edit-component',
  templateUrl: './invoice.edit.component.html'
})
export class InvoiceEditComponent { 
  public constructor(private route:ActivatedRoute, private router:Router) {
    let s = route.snapshot.data['id'];
    console.log("extras state:");
    console.log(s);
  }
}
