// Theme generated via https://coolors.co/
export enum ThemeColor {
  lightest = "#C2D8B9",
  lighter = "#E4F0D0",
  base = "#FFFCF7",
  dark = "#A1B5D8",
  darkest = "#738290",
}

/**
 * Contrasting colors that are at least WCAG AA compliant based on the theme
 */
export enum ThemeContrastColor {
  // always has been
  contrastLightest = "#12263A",
  contrastLighter = "#12263A",
  contrastBase = "#12263A",
  contrastDark = "#12263A",
  contrastDarkest = "#000000",
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
