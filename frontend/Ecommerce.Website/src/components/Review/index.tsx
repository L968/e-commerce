import moment from 'moment';
import { Header } from './styles';
import Card from '@mui/material/Card';
import Review from '@/interfaces/Review';
import { CardContent, Rating } from '@mui/material';

export default function ReviewComponent(review: Review) {
    return (
        <Card>
            <CardContent>
                <Header>
                    <Rating value={review.rating} readOnly />
                    <span>{moment(review.createdAt).format('MMM DD, YYYY')}</span>
                </Header>
                <p>{review.description}</p>
            </CardContent>
        </Card>
    )
}
