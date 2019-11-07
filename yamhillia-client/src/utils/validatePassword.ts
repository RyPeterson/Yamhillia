import isEmpty from "validator/lib/isEmpty";

function hasDigit(pwd: string): boolean {
  return /\d/g.test(pwd);
}

function hasNonAlphanumeric(pwd: string): boolean {
  return /^.*[^a-zA-Z0-9].*$/.test(pwd);
}
export default function validatePassword(pwd: string): boolean {
  if (isEmpty(pwd) || pwd.length < 6) {
    return false;
  }

  return (
    hasDigit(pwd) && hasNonAlphanumeric(pwd) && new Set(pwd.split("")).size >= 3
  );
}
