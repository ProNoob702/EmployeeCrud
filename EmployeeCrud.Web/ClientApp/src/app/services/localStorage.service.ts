import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class LocalStorageService {
  constructor() {}

  setInfo<T>(key: string, data: T) {
    const jsonData = JSON.stringify(data);
    localStorage.setItem(key, jsonData);
  }

  loadInfo<T>(key: string): T | null {
    const foundInfo = localStorage.getItem(key);
    const data = foundInfo ? JSON.parse(foundInfo) : null;
    return data as T | null;
  }

  clearInfo(key: string) {
    localStorage.removeItem(key);
  }
}
