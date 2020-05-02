import { css } from "styled-components/macro";

interface HideableProps {
  hide?: boolean;
}

const hideable = () => {
  return function(props: HideableProps) {
    const { hide } = props;
    return css`
      ${hide
        ? css`
            opacity: 0;
            pointer-events: none;
          `
        : css`
            opacity: 1;
          `}
    `;
  };
};

export default hideable;
