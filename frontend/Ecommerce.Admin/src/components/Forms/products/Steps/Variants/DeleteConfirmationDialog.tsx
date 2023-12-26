import { Dialog, DialogTitle, DialogContent, DialogActions, Button } from '@mui/material';

interface DeleteConfirmationDialogProps {
    open: boolean
    onClose: () => void
    onConfirm: () => void
}

export default function DeleteConfirmationDialog({ open, onClose, onConfirm }: DeleteConfirmationDialogProps) {
    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>Confirm Deletion</DialogTitle>
            <DialogContent>
                Are you sure you want to delete this variant?
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose} color='error'>
                    Cancel
                </Button>
                <Button onClick={onConfirm} color='primary' variant='contained'>
                    Confirm
                </Button>
            </DialogActions>
        </Dialog>
    )
}
