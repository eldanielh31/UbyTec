export class CustomersModelServer {
  identification: number;
  firstName: string;
  lastName: string;
  lastName2: string;
  phone: string;
  dob: Date;
  id_address: string;
  username: string;
  password: string;
}

export interface CustomerResponse {
  count: number;
  customers: CustomersModelServer[];
}
