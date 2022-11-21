import { Injectable } from '@angular/core';

import pdfMake from "pdfmake/build/pdfmake";
import pdfFonts from "pdfmake/build/vfs_fonts";
pdfMake.vfs = pdfFonts.pdfMake.vfs;


class Product {
  identification: number;
  total: number;
  qty: number;
}
class Invoice {
  customerName: string;
  address: string;
  contactNo: number;
  email: string;

  products: Product[] = [];
  additionalDetails: string;

  constructor() {
    // Initially one empty product row we will show     
    this.products.push(new Product());
  }
}

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  invoice = new Invoice();

  constructor() { }

  public generatePDF() {

    let docDefinition = {

      content: [
        {
          text: 'UbyTEC',
          fontSize: 16,
          alignment: 'center',
          color: '#047886'
        },
        {
          text: 'Report',
          fontSize: 20,
          bold: true,
          alignment: 'center',
          decoration: 'underline',
          color: 'skyblue'
        },
        {
          text: 'User Details',
          style: 'sectionHeader'
        },
        {
          columns: [
            [
              {
                text: this.invoice.customerName,
                bold: true
              },
              { text: this.invoice.address },
              { text: this.invoice.email },
              { text: this.invoice.contactNo }
            ],
            [
              {
                text: `Date: ${new Date().toLocaleString()}`,
                alignment: 'right'
              },
              {
                text: `Bill No : ${((Math.random() * 1000).toFixed(0))}`,
                alignment: 'right'
              }
            ]
          ]
        },

        {
          text: 'Report Details',
          style: 'sectionHeader'
        },
        {
          table: {
            headerRows: 1,
            widths: ['*', 'auto', 'auto'],
            body: [
              ['Identification', 'Quantity', 'TOTAL'],
              ...this.invoice.products.map(p => ([p.identification, p.qty, p.total])),
            ]
          }
        },
        {
          ul: [
            'Report can be return in max 10 days.',
            'Warrenty of the product will be subject to the manufacturer terms and conditions.',
            'This is system generated report.',
          ],
        }

      ],
      styles: {
        sectionHeader: {
          bold: true,
          decoration: 'underline',
          fontSize: 14,
          margin: [0, 15, 0, 15]
        }
      }


    }
    pdfMake.createPdf(docDefinition).open();
  }

}