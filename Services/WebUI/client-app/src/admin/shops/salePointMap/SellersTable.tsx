import {Stack, Table, TableBody, TableCell, TableHead, TableRow, Tooltip, Typography} from "@mui/material";
import {Seller} from "../../../global/models/shop";

interface Props{
    sellers: Seller[]
}

export function SellersTable({sellers}: Props) {
    return (
        <Table>
            <TableHead>
                <TableRow>
                    <TableCell style={{textAlign: 'center'}}>
                        <strong>Imię</strong>
                    </TableCell>
                    <TableCell style={{textAlign: 'center'}}>
                        <strong>Nazwisko</strong>
                    </TableCell>
                    <TableCell style={{textAlign: 'center'}}>
                        <strong>Numer telefonu</strong>
                    </TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {sellers.map((s, i) => (
                    <TableRow key={i} style={{textAlign: 'center'}}>
                        <TableCell style={{textAlign: 'center'}}>{s.firstName}</TableCell>
                        <TableCell style={{textAlign: 'center'}}>{s.lastName}</TableCell>
                        <TableCell style={{textAlign: 'center'}}>{s.phoneNumber}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
}