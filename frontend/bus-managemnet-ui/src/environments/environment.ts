import packageInfo from '../../package.json';

export const environment = {

  appVersion: packageInfo.version,
  production: false,
  baseUrl: 'https://localhost:7242/api/',
  reportUrl: 'https://localhost:7242/',
  signalRUrl: 'https://localhost:7242/chathub',
  appsApiUrl: 'http://localhost:5103/api/apps/v1.0/',
  // uploadFileUrl: 'D:/project/Inventory-Front/src/assets/images/uploadedImages'
};