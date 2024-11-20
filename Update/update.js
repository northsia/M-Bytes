const ffi = require('ffi-napi');
const ref = require('ref-napi');
const path = require('path');

// Function to get the version of the executable
function get_exec_ver(appPath) {
  const kernel32 = ffi.Library('kernel32.dll', {
    'GetFileVersionInfoSizeW': ['uint32', ['string']],
    'GetFileVersionInfoW': ['uint32', ['string', 'uint32', 'uint32', 'pointer']],
    'VerQueryValueW': ['bool', ['pointer', 'string', 'pointer', 'pointer']],
  });

  // Get the file version info size
  const size = kernel32.GetFileVersionInfoSizeW(appPath);
  if (size === 0) {
    console.error('Error retrieving version info size');
    return null;
  }

  const buffer = Buffer.alloc(size);
  const pointer = ref.alloc('pointer');

  // Get the file version info
  kernel32.GetFileVersionInfoW(appPath, 0, size, buffer);

  // Get the version
  const versionPointer = ref.alloc('pointer');
  let len = ref.alloc('uint32');
  kernel32.VerQueryValueW(buffer, '\\', versionPointer, len);

  const versionInfo = versionPointer.deref();
  const version = versionInfo.readCString();

  return version;
}

// Main function to check version
async function checkVersion() {
  const appPath = path.join(__dirname, 'M-Bytes.exe');

  try {
    const version = await GET_VER();  // Firebase version (same as before)
    if (!version) {
      console.log('No version data from Firebase');
      return;
    }

    const appVersion = get_exec_ver(appPath);  // Get the executable version

    if (version !== appVersion) {
      console.log('Firebase version:', version);
      console.log('Executable version:', appVersion);
      console.log('Versions are different!');
      // Do something if versions are different
    } else {
      console.log('Versions are the same.');
    }
  } catch (error) {
    console.error('Error:', error);
  }
}

checkVersion();
