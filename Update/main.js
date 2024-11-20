const { app, BrowserWindow } = require('electron');
const path = require('node:path');

function createWindow() {
  const win = new BrowserWindow({
    width: 600,
    height: 400,
    frame: false,
    webPreferences: {
      nodeIntegration: true,  // Enable Node.js in renderer process
      contextIsolation: false,  // Disable context isolation to allow access to Node.js APIs
    }
  });

  win.webContents.on('did-finish-load', () => {
    win.webContents.insertCSS('body { background-color: #121212; color: white; }');
  });

  win.loadFile('main.html');

 // win.webContents.openDevTools();
}

app.whenReady().then(() => {
  createWindow();

  app.on('activate', () => {
    if (BrowserWindow.getAllWindows().length === 0) {
      createWindow();
    }
  });
});

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit();
  }
});
