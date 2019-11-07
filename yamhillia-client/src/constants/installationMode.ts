const configMode = process.env.REACT_APP_INSTALLATION_MODE;

export enum InstallationModes {
  /* Accessible to the public */
  INTERNET,
  /* Accessible locally to an organization/farm */
  INTRANET
}

export default function getInstallationMode(): InstallationModes {
  if (configMode === "intranet") {
    return InstallationModes.INTRANET;
  }
  return InstallationModes.INTERNET;
}
