import {Stack, Table, TableBody, TableCell, TableHead, TableRow, Typography} from "@mui/material";
import {ShopWorkingDay} from "../../../global/models/shop";
import {TimeRange, WeekDay} from "../../../global/models/common";

interface Props{
    workingDays: ShopWorkingDay[]
}

export function OpenHoursTable({workingDays}: Props) {
    return (
        <Table>
            <TableHead>
                <TableRow>
                    <TableCell style={{textAlign: 'center'}}>
                        <strong>Dzień tygodnia</strong>
                    </TableCell>
                    <TableCell style={{textAlign: 'center'}}>
                        <strong>Godziny otwarcia</strong>
                    </TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {workingDays.map((d, i) => (
                    <TableRow key={i}>
                        <TableCell style={{textAlign: 'center'}}>{d.weekDay.value}</TableCell>
                        <TableCell style={{textAlign: 'center'}}>
                            {!d.isClosed && d.openHours ?
                                <Typography>{`${d.openHours.start} - ${d.openHours.end}`}</Typography>
                                :
                                d.cachedOpenHours ?
                                    <Typography style={{textDecoration: 'line-through'}}>{`${d.cachedOpenHours.start} - ${d.cachedOpenHours.end}`}</Typography>
                                    :
                                    'Zamknięte'
                            }
                        </TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
}