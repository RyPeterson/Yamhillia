import { useCallback, useEffect, useState } from "react";

export default function useLoginForm() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleCleared = useCallback(() => {
    setEmail("");
    setPassword("");
  }, []);

  useEffect(() => {
    handleCleared();
  }, [handleCleared]);

  return {
    email,
    onEmailChanged: setEmail,
    password,
    onPasswordChanged: setPassword,
    reset: handleCleared,
  };
}
