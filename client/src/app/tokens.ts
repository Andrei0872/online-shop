import { InjectionToken } from "@angular/core";

import { environment } from '../environments/environment';

export interface Environment {
  API_URL: string;
};

export const ENV_CONFIG = new InjectionToken('ENV_CONFIG', {
  providedIn: 'root',
  factory: () => environment,
});