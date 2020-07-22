// Theme generated via https://coolors.co/
export enum ThemeColor {
  lightest = "#c8ffbe",
  lighter = "#edffab",
  base = "#ba9593",
  dark = "#89608e",
  darkest = "#623b5a",
}

/**
 * Contrasting colors that are at least WCAG AA compliant based on the theme
 */
export enum ThemeContrastColor {
  contrastLightest = "#0A4200",
  contrastLighter = "#0A4200",
  contrastBase = "#000000",
  contrastDark = "#FFFFFF",
  contrastDarkest = "#FFFFFF",
}

export const getContrastingColor = (
  themeColor: ThemeColor
): ThemeContrastColor => {
  switch (themeColor) {
    case ThemeColor.base:
      return ThemeContrastColor.contrastBase;
    case ThemeColor.dark:
      return ThemeContrastColor.contrastDark;
    case ThemeColor.darkest:
      return ThemeContrastColor.contrastDarkest;
    case ThemeColor.lightest:
      return ThemeContrastColor.contrastLightest;
    case ThemeColor.lighter:
      return ThemeContrastColor.contrastLighter;
    default:
      throw new Error(`${themeColor} does not have a contrasting color`);
  }
};

export const error = "#FF3D3D";
