export class TickerModel {
  id: string;
  company: string;
  value: string;
  date: string;

  constructor(id: string, company: string, dateTime: string, value: string) {
    this.id = id;
    this.company = company;
    this.date = dateTime;
    this.value = value;
  }
}
