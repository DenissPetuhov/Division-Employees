import React, { FC, PropsWithChildren } from "react";
import { createPortal } from "react-dom";
import './index.css';


interface IModalProps {
  open: boolean,
  title: string,
  onClose: () => void

}

const Modal: FC<PropsWithChildren<IModalProps>> = ({ open, title, onClose, children }) => {
  return open ? createPortal(
    <div className="wrap">
      <div className="modalContainer">
        <div className="header">
          <div>{title}</div>
          <button onClick={onClose}>X</button>
        </div>
        {children}
      </div>
    </div>
    , document.body) : null;
};

export default Modal;