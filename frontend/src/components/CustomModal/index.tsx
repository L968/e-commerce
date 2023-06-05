import Modal from "react-modal";

interface CustomModalProps {
  children: JSX.Element;
  isOpen: boolean;
  setIsOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

export const CustomModal = ({
  children,
  isOpen,
  setIsOpen,
}: CustomModalProps): JSX.Element => {
  return (
    <Modal isOpen={isOpen} onRequestClose={() => setIsOpen(false)}>
      {children}
    </Modal>
  );
};
