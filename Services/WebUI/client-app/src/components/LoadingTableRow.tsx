import {Skeleton, TableCell, TableRow} from "@mui/material";
import {number} from "yup";

interface Props {
    cells: number
    rows: number
}

const LoadingTableRow = ({cells, rows}: Props) => {
    const placeholderRows = new Array(rows).fill(null).map((_, rowIndex) => (
        <TableRow key={rowIndex}>
            {new Array(cells).fill(null).map((_, cellIndex) => (
                <TableCell
                    key={cellIndex}
                    style={{ textAlign: 'center', width: `${100 / cells}%` }}
                >
                    <Skeleton variant="text" width="80%" height={30} />
                </TableCell>
            ))}
        </TableRow>
    ));

    return <>{placeholderRows}</>
};

export default LoadingTableRow