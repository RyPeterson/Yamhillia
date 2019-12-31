export const widePX = 1440;
export const desktopMaxPX = 1439;
export const desktopMinPX = 1024;
export const tabletMaxPX = 1023;
export const tabletMinPX = 769;
export const mobileMaxPX = 768;

export const wideMin = `(min-width: ${widePX}px)`;
export const desktopMax = `(max-width: ${desktopMaxPX}px)`;
export const desktopMin = `(min-width: ${desktopMinPX}px)`;
export const tabletMax = `(max-width: ${tabletMaxPX}px)`;
export const tabletMin = `(min-width: ${tabletMinPX}px)`;
export const mobileMax = `(max-width: ${mobileMaxPX}px)`;

export const wide = wideMin;
export const desktop = `${desktopMax} and ${desktopMin}`;
export const tablet = `${tabletMax} and ${tabletMin}`;
export const mobile = mobileMax;
