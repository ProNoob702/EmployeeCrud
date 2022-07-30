export interface IEmployee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
}

export interface IEmployeeWithoutId {
  firstName: string;
  lastName: string;
  email: string;
}
