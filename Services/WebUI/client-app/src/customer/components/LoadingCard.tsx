import { Card, Skeleton, CardContent, CardActions } from "@mui/material";

const LoadingCard: React.FC = () => {
  return (
    <Card style={{ padding: '15px', position: 'relative', height: '400px' }}>
      <Skeleton variant='rectangular' height={200} />
      <CardContent style={{ paddingBottom: '0px' }}>
        <Skeleton height={32} />
        <Skeleton />
        <Skeleton width="70%" height={40} style={{ margin: '10px 0px 0px 0px', borderRadius: '30px' }} />
      </CardContent>
      <CardActions style={{ justifyContent: 'center' }}>
        <Skeleton variant='circular' width={40} height={40} />
      </CardActions>
    </Card>
  );
}

export default LoadingCard;
