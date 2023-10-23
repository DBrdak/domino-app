import { TextField, Button, Box, Stack } from "@mui/material";
import { useState } from "react";

interface EmailFormProps {
    onSubmit: (email: string, message: string) => void;
}

const EmailForm: React.FC<EmailFormProps> = ({ onSubmit }) => {
    const [email, setEmail] = useState('');
    const [message, setMessage] = useState('');

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        onSubmit(email, message);
    };

    return (
        <Box component="form" noValidate autoComplete="off" onSubmit={handleSubmit}>
        <Stack direction={'column'}>
            <TextField id="outlined-basic" label="Email" variant="outlined" value={email} onChange={(e) => setEmail(e.target.value)} />
            <TextField id="outlined-multiline-static" label="Wiadomość" multiline rows={8} 
            variant="outlined" value={message} style={{margin: '10px 0px 10px 0px'}}
            onChange={(e) => setMessage(e.target.value)} />
            <Button type="submit" variant="contained">Wyślij</Button>
        </Stack>
        </Box>
    );
}

export default EmailForm;
